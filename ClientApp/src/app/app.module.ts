import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule, MatToolbarModule, MatButtonModule, MatInputModule, MatCardModule, MatTooltipModule, MatRippleModule, MatSnackBarModule } from '@angular/material';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './pages/home/home.component';
import { ExploreComponent } from './pages/explore/explore.component';
import { PublishComponent } from './pages/publish/publish.component';
import { FilesComponent } from './pages/files/files.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ExploreComponent,
    PublishComponent,
    FilesComponent,
    FileUploadComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'explore', component: ExploreComponent, },
      { path: 'publish', component: PublishComponent, },
      { path: 'files', component: FilesComponent, },
    ]),
    BrowserAnimationsModule,
    FlexLayoutModule,
    // material modules
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatTooltipModule,
    MatSnackBarModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
