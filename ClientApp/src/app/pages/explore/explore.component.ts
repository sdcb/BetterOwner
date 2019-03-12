import { MatDialog } from '@angular/material';
import { Component, OnInit } from '@angular/core';
import { PublishComponent } from '../publish/publish.component';

@Component({
  selector: 'app-explore',
  templateUrl: './explore.component.html',
  styleUrls: ['./explore.component.css']
})
export class ExploreComponent implements OnInit {

  constructor(private dialogService: MatDialog) { }

  ngOnInit() {
  }

  openPublish() {
    PublishComponent.openDialog(this.dialogService).afterClosed().subscribe(x => {
      console.log(x);
    });
  }
}
