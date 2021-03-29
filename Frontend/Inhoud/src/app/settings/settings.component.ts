import { Component, OnInit } from '@angular/core';
import { BackendService } from '../backend.service';
import {Settings} from '../Settings'

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
  }

}
