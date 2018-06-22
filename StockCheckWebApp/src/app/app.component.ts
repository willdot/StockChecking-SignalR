import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

import { Message } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  private _hubConnection: HubConnection;
  msgs: Message[] = [];

  constructor() {}

  ngOnInit(): void {

    this._hubConnection = new HubConnectionBuilder()
    .withUrl('https://localhost:44398/stock')
    .build();

    this._hubConnection
    .start()
  .then(() => console.log('It started'))
  .catch(err => console.log('Error'));

    this._hubConnection.on('BroadcastMessage', (value: string) => {
      this.msgs.push({summary: value});
    });
  }
}
