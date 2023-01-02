import * as THREE from 'three';
import { ElementRef, Injectable, NgZone, OnDestroy } from '@angular/core';








@Injectable({ providedIn: 'root' })
export class EngineService implements OnDestroy
{

  // Properties
  private canvas!: HTMLCanvasElement;
  private renderer!: THREE.WebGLRenderer;
  private camera!: THREE.PerspectiveCamera;
  private scene!: THREE.Scene;
  private light!: THREE.AmbientLight;
  private cube!: THREE.Mesh;
  private frameId: number = 0;




  // Dependency Injection
  public constructor(private ngZone: NgZone) {}

  public ngOnDestroy(): void {
    if (this.frameId != null) {
      cancelAnimationFrame(this.frameId);
    }
    if (this.renderer != null) {
      this.renderer.dispose();
      this.renderer.clear();
      this.canvas.remove();
    }
  }

  public createScene(canvas: ElementRef<HTMLCanvasElement>): void
  {
    this.canvas = canvas.nativeElement; // Get reference of canvas from HTML doc
    //this.canvas.style.display = "block";
    //this.canvas.style.margin = "auto";

    this.renderer = new THREE.WebGLRenderer({
      
      canvas: this.canvas,
      alpha: false,    // transparent background
      antialias: false // smooth edges
    });

    this.renderer.setSize(window.innerWidth/2, window.innerHeight/2);
    //this.renderer.setSize(window.innerWidth, window.innerHeight);

    // 1. Setting up the camera
    this.camera = new THREE.PerspectiveCamera(
      75,
      window.innerWidth / window.innerHeight,
      0.1, 1000
    );
    this.camera.position.z = 5;

    // 2. Setting up the light
    this.light = new THREE.AmbientLight(0x404040);  // soft white light
    this.light.position.z = 10;

    // 3. Setting up the geometry
    const geometry = new THREE.BoxGeometry(1, 1, 1);
    const material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    this.cube = new THREE.Mesh(geometry, material);

    // Adding everything to the Scene
    this.scene = new THREE.Scene();
    this.scene.add(this.camera);
    this.scene.add(this.light);
    this.scene.add(this.cube);
  }

  public animate(): void {
    // We have to run this outside angular zones,
    // because it could trigger heavy changeDetection cycles.
    this.ngZone.runOutsideAngular(() => {
      if (document.readyState !== 'loading') {
        this.render();
      } else {
        window.addEventListener('DOMContentLoaded', () => {
          this.render();
        });
      }

      window.addEventListener('resize', () => {
        this.resize();
      });
    });
  }

  public render(): void {
    this.frameId = requestAnimationFrame(() => {
      this.render();
    });
    this.cube.rotation.x += 0.01;
    this.cube.rotation.y += 0.01;
    this.renderer.render(this.scene, this.camera);
  }

  public resize(): void {
    //const width = window.innerWidth;
    //const height = window.innerHeight;

    //this.camera.aspect = width / height;
    //this.camera.updateProjectionMatrix();

    //this.renderer.setSize(width, height);
  }
}
