import { Component, ElementRef, NgZone, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Scene, PerspectiveCamera } from 'three';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import { DRACOLoader } from 'three/examples/jsm/loaders/DRACOLoader';
import * as DAT from 'dat.gui';

@Component({
  selector: 'app-test-page-five',
  templateUrl: './test-page-five.component.html',
  styleUrls: ['./test-page-five.component.css']
})
export class TestPageFiveComponent implements OnInit {

  constructor(private ngZone: NgZone) { }

  // Global Variables
  scene?: THREE.Scene;
  camera?: THREE.PerspectiveCamera;
  renderer?: THREE.Renderer;
  canvas?: HTMLCanvasElement;
  box?: THREE.Mesh;
  ambient?: THREE.AmbientLight;
  light?: THREE.DirectionalLight;
  controls?: OrbitControls;
  loader?: GLTFLoader;
  path?: string;
  aspect?: number;
  cameras?: [];

  //////////////////////////////////////////////////
  // Test Variables

  gui?: DAT.GUI;
  //////////////////////////////////////////////////


  @ViewChild('rendererCanvas', { static: true })
  rendererCanvas!: ElementRef<HTMLCanvasElement>;

  ngOnInit(): void {

    this.aspect = window.innerWidth / window.innerHeight;

    this.ThreeJsInit();









  }

  ThreeJsInit() {
    // Canvas Setup
    this.canvas = this.rendererCanvas.nativeElement;
    // Renderer Setup
    this.renderer = new THREE.WebGLRenderer({
      canvas: this.canvas,
      alpha: true,
      antialias: false
    });
    this.renderer.setSize(window.innerWidth / 1.5, window.innerHeight / 1.5);

    // Camera Setup
    var near = 0.1;
    var far = 50;
    this.camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, near, far);
    this.camera.position.set(-2, 1, 0);
    this.camera.rotation.set(1, -1, -1);


    // Light Setup - Light & Ambient
    this.ambient = new THREE.AmbientLight(0x404040, 5);
    this.light = new THREE.DirectionalLight(0xFFFFFF, 1);
    this.light.position.set(1, 10, 10);

    // Controls Setup
    this.controls = new OrbitControls(this.camera, this.renderer.domElement)
    this.controls.target.set(0, 0, 0);
    this.controls.update();

    // Geometry Setup
    const height = 0.4;
    const geometry = new THREE.BoxGeometry(3, height, 0.9);
    const material = new THREE.MeshLambertMaterial({ color: new THREE.Color('skyblue') });
    this.box = new THREE.Mesh(geometry, material);

    // Loader
    this.path = "../../../assets/models/poly_4.glb"
    this.loader = new GLTFLoader();
    this.loader.load
      (
        this.path,
        object => this.scene!.add(object.scene)
      )

    // Adding everything to the Scene
    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(0xaaaaaa);
    this.scene.add(this.camera);
    this.scene.add(this.ambient);
    this.scene.add(this.light);

    window.addEventListener('resize', () => this.onResize(), false);

    this.CamHelper(near, far);

    // Animation - Recursive Function
    this.update();
  }

  // Resizing Function
  onResize() {
    this.camera!.aspect = window.innerWidth / window.innerHeight;
    this.camera?.updateProjectionMatrix();
    this.renderer!.setSize(window.innerWidth / 1.5, window.innerHeight / 1.5);
  }

  // Recursive Function
  update(): void {
    requestAnimationFrame(() => this.update())
    //this.box!.rotation.y += 0.010;
    //this.box!.rotation.x += 0.01;
    this.renderer!.render(this.scene!, this.camera!);
  }


  CamHelper(near: number, far: number) {
    const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, near, far);
    camera.position.set(0, 0, 15);
    const helper = new THREE.CameraHelper(camera);
    this.scene!.add(helper);
    helper.visible = true;
  }
}
