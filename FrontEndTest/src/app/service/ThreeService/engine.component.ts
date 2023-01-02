import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { EngineService } from './engine.service';

@Component({
  selector: 'app-engine',
  templateUrl: './engine.component.html',
    styleUrls: ['./engine.component.css']
})

export class EngineComponent implements OnInit {

  // Property
  @ViewChild('rendererCanvas', { static: true }) rendererCanvas!: ElementRef<HTMLCanvasElement>;

  // Injecting the engine service into the class
  public constructor(private engServ: EngineService) {
  }

  public ngOnInit(): void {
    this.engServ.createScene(this.rendererCanvas);  // Putting the ViewChild Property in
    this.engServ.animate();
  }

}
