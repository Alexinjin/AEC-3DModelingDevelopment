var scene, root, renderer, camera, controls, exactdatetime, sunlight;
var canvas = document.getElementById("canvas");
init();
createScene();
animate();

function init () {
	scene = new THREE.Scene();
	scene.fog = new THREE.FogExp2( 0xaaacaf, 0.04 );
	root = new THREE.Object3D();
	scene.add( root );

	renderer = new THREE.WebGLRenderer();
	renderer.setSize( window.innerWidth, window.innerHeight );
	renderer.shadowMap.enabled = true;
	canvas.appendChild( renderer.domElement );

	camera = new THREE.PerspectiveCamera(
		75,
		window.innerWidth / window.innerHeight,
		0.1,
		1000.0
	);

	camera.position.z = 50.0;
	controls = new THREE.OrbitControls(camera, document.getElementById("canvas"));
	controls.addEventListener( "change", render );
}

function animate () {
	requestAnimationFrame( animate );
	controls.update();
	render();
}

function render () {
	renderer.render(scene, camera);
}

function createScene () {
	// Create the objects
	// In this case z+ is north, y- is the gravity.
	var ground = createBox( 50.0, 1.0, 50.0, 0x99ff99 );
	ground.position.set( 0.0, 0.0, 0.0 );
	root.add( ground );
	// Cube in the middle
	var cube = createBox( 10.0, 5.0, 10.0 );
	cube.position.set( 0.0, 2.5, 0.0 );
	root.add( cube );
	// Obstacle
	var obstacle = createBox( 5.0, 10.0, 5.0 );
	obstacle.position.set( 0.0, 5, -5 );
	root.add( obstacle );
	// North pole indicator
	var northIndicator = createBox( 2.0, 2.0, 8.0, 0xff0000 );
	northIndicator.position.set( 0.0, 1.0, 15.0 );
	root.add( northIndicator );

	// Add an ambient light
	root.add( new THREE.AmbientLight( 0x333333) );

	// Create the sun light and add it to the scene
	var sunLight = new SunLight(
		// Oulu
		new THREE.Vector2( 33.65, -117.83 ),
		new THREE.Vector3( 0.0, 0.0, 1.0 ),
		new THREE.Vector3( -1.0, 0.0, 0.0 ),
		new THREE.Vector3( 0.0, -1.0, 0.0 ),
	);
	root.add( sunLight );

	sunlight = 6;
	sunLight.updateOrientation(true, new Date(2015, 11, 01, 8, 0));
	sunLight.updateDirectionalLight();

	// Adjust the directional light's shadow camera dimensions
	sunLight.directionalLight.shadow.camera.right = 30.0;
	sunLight.directionalLight.shadow.camera.left = -30.0;
	sunLight.directionalLight.shadow.camera.top = 30.0;
	sunLight.directionalLight.shadow.camera.bottom = -30.0;
}

function createBox ( width_, height_, depth_, color_ = 0xffffff ) {
	var geometry = new THREE.BoxGeometry( width_, height_, depth_ );
	var material = new THREE.MeshPhongMaterial( { color: color_ } );
	var cube = new THREE.Mesh( geometry, material );
	cube.castShadow = true;
	cube.receiveShadow = true;
	return cube;
}

function gettime(datetime){
	var datetime = datetime.split("T");
	var date = datetime[0].split("-");
	var time = datetime[1].split(":");
  exactdatetime = new Date(date[0], date[1]-1, date[2], time[0], time[1]);
	root.getObjectByName("sunlight").updateOrientation(true, exactdatetime);
	root.getObjectByName("sunlight").updateDirectionalLight();
	// console.log(sunlight);
	// console.log(root.getObjectByName("sunlight"));
}
