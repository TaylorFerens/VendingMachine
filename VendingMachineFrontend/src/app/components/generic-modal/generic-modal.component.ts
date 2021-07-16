import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
  content: string;
  header: string;
  confirm: string;
  cancel: string;
}

@Component({
  selector: 'app-generic-modal',
  templateUrl: './generic-modal.component.html',
  styleUrls: ['./generic-modal.component.scss']
})
export class GenericModalComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData,
private dialogRef: MatDialogRef<GenericModalComponent>  ) { }

  ngOnInit() {
  }

  Confirm() {
    this.dialogRef.close(true);
  }

  Cancel() {
    this.dialogRef.close(false);
  }

}
