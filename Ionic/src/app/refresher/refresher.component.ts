import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { Observable, of } from 'rxjs';


enum Spinners {
  ios = 'ios',
  ios_small = 'ios-small',
  bubbles = 'bubbles',
  circles = 'circles',
  crescent = 'crescent',
  dots = 'dots'
}

@Component({
  selector: 'refresher',
  templateUrl: './refresher.component.html',
  styleUrls: ['./refresher.component.scss'],
  encapsulation: ViewEncapsulation.None // makes the styles in ./refresher.component.scss visible from anywhere and not only from refresher.component
})
export class RefresherComponent implements OnInit {

  @Input() pullingIcon: string = "refresh"; // expects an ion-icon
  @Input() pullingText: string = "";
  @Input() refreshingSpinner: string = Spinners.crescent;
  @Input() refreshingText: string = "";
  @Input() awaitFor: Observable<any> = of(null);

  constructor() { }

  ngOnInit() {}

  handleStart(event) {}

  handlePull(event) {}

  handleRefresh(event) {
    this.awaitFor.subscribe().add( () => event.target.complete() );
  }
}
