import { ExploreApiService, TreasureExploreDto } from './explore.api';
import { MatDialog } from '@angular/material';
import { Component, OnInit } from '@angular/core';
import { PublishComponent } from '../publish/publish.component';
import { GlobalLoadingService } from 'src/app/services/global-loading.service';

@Component({
  selector: 'app-explore',
  templateUrl: './explore.component.html',
  styleUrls: ['./explore.component.css']
})
export class ExploreComponent implements OnInit {
  treasures = Array<TreasureExploreDto>();

  constructor(
    private dialogService: MatDialog,
    private api: ExploreApiService,
    private loading: GlobalLoadingService) { }

  async ngOnInit() {
    await this.loading.wrap(this.api.treasures({}).toPromise());
  }

  openPublish() {
    PublishComponent.openDialog(this.dialogService).afterClosed().subscribe(x => {
      console.log(x);
    });
  }
}
