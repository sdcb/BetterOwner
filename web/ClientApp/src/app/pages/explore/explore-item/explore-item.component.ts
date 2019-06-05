import { Component, OnInit, Input } from '@angular/core';
import { TreasureExploreDto } from '../explore.api';

@Component({
  selector: 'app-explore-item',
  templateUrl: './explore-item.component.html',
  styleUrls: ['./explore-item.component.css']
})
export class ExploreItemComponent implements OnInit {
  @Input()
  data: TreasureExploreDto;

  constructor() { }

  ngOnInit() {
  }

}
