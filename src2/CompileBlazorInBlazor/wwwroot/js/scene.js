var scene;
var camera;
var cube;
var renderer;
var material;
var raycaster = new THREE.Raycaster();
var mouse = new THREE.Vector2();
var caller;
var controls;

function createScene(reference) {
    window.addEventListener('click', onDocumentMouseDown, false);
    caller = reference;
    scene = new THREE.Scene();

    scene.background = new THREE.Color(0xfafafa);

    camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

    camera.position.set(0, 0, 50);

    camera.up = new THREE.Vector3(0, 0, 1);
    camera.lookAt(new THREE.Vector3(0, 0, 0));

    renderer = new THREE.WebGLRenderer({
        canvas: document.querySelector("canvas"),
        antialias: true,
        preserveDrawingBuffer: true
    });
    renderer.setSize(window.innerWidth, window.innerHeight);

    var size = 50;
    var divisions = 25;

    var gridHelper = new THREE.GridHelper(size, divisions);
    gridHelper.name = "GridHelper";
    gridHelper.geometry.rotateX(Math.PI / 2);
    scene.add(gridHelper);

    controls = new THREE.OrbitControls(camera, renderer.domElement);

}

window.addEventListener('resize', onWindowResize, false);

function onWindowResize() {

    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(window.innerWidth, window.innerHeight);

}

function onDocumentMouseDown(event) {
    event.preventDefault();
    mouse.x = (event.clientX / renderer.domElement.clientWidth) * 2 - 1;
    mouse.y = - (event.clientY / renderer.domElement.clientHeight) * 2 + 1;
    raycaster.setFromCamera(mouse, camera);
    var intersects = raycaster.intersectObjects(scene.children);

    if (intersects.length > 0) {
        intersects.forEach(x => {
            if (x.object.callback) {
                x.object.callback();
            }
        });

    }
}

function addCube(x, y, z, width, depth, height, color) {
    var geometry = new THREE.BoxGeometry(x, y, z);
    var color1 = new THREE.Color(color);
    material = new THREE.MeshBasicMaterial({ color: color1 });
    cube = new THREE.Mesh(geometry, material);
    //cube.callback = function () { caller.invokeMethodAsync('OnClickCube', cube); };
    scene.add(cube);
    console.log("Cube Added");
}

function addSphere(r, u, v, color) {
    var geometry = new THREE.SphereGeometry(r, u, v);
    var color1 = new THREE.Color(color);
    material = new THREE.MeshBasicMaterial({ color: color1 });
    sphere = new THREE.Mesh(geometry, material);
    //cube.callback = function () { caller.invokeMethodAsync('OnClickCube', cube); };
    scene.add(sphere);
    console.log("Sphere Added");
}

function createCube() {
    var geometry = new THREE.BoxGeometry(10, 10, 10);
    material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    cube = new THREE.Mesh(geometry, material);
    //cube.callback = function () { caller.invokeMethodAsync('OnClickCube', cube); };
    scene.add(cube);
    //camera.position.z = 5;
}

function healthy() {
    cube.material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
}

function unhealthy() {
    cube.material = new THREE.MeshBasicMaterial({ color: 0xff0000 });
}

function clickCube() {
    cube.material = new THREE.MeshBasicMaterial({ color: getRandomColor() });
}

function clearScene() {

    var tempList = [];
    for (var i = 0; i < scene.children.length; i++) {
        var child = scene.children[i];
        tempList.push(child);
    }

    for (var i = 0; i < tempList.length; i++) {
        var currObj = tempList[i];

        if (currObj.name != "GridHelper") {
            scene.remove(currObj);
        }
    }
}

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}


var animate = function() {
    requestAnimationFrame(animate);
    controls.update();
    //cube.rotation.x += 0.01;
    //cube.rotation.y += 0.01;
    renderer.render(scene, camera);
};
