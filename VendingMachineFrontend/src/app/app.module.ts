import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import {MatDialog, MatDialogModule} from '@angular/material/dialog'
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeScreenComponent } from './components/home-screen/home-screen.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GenericModalComponent } from './components/generic-modal/generic-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeScreenComponent,
    GenericModalComponent
  ],
  imports: [
    MatDialogModule,
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    MatInputModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  entryComponents:[GenericModalComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
