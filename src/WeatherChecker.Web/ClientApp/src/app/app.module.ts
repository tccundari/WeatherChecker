import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatToolbarModule, MatIconModule, MatTooltipModule, MatButtonModule, MatCardModule, MatGridListModule, MatInputModule, MatSelectModule } from "@angular/material";
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { NotificationListComponent } from './notification.component';
import { NotificationService } from './notification.service';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CorsInterceptor } from './cors.interceptor';

@NgModule({ 
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    NotificationListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    FlexLayoutModule,
    MatToolbarModule, MatIconModule, MatTooltipModule, MatButtonModule, MatCardModule, MatGridListModule, MatInputModule, MatSelectModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [NotificationService, {
    provide: HTTP_INTERCEPTORS,
    useClass: CorsInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
