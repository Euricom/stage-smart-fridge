import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  Min1: number;
  Min2: number;

  Color1: String;
  Color2: String;

  SendAmount: number;
  AllowSend: boolean;

  constructor() { }

  ngOnInit(): void {
  }

}