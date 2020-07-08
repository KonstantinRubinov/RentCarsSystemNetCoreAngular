import { Component, OnInit, Input } from '@angular/core';
import { Branch } from 'src/app/models/Branch';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  @Input() branch:Branch;
  constructor() { }

  ngOnInit() {
  }
}
