import { Component } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { DailyConsolidatedComponent } from './components/daily-consolidated/daily-consolidated.component'
import { CashflowComponent } from './components/cashflow/cashflow.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'ChallengeCrf-Angular';
  private _hubConnection: HubConnection;
  private _compDC: DailyConsolidatedComponent;
  private _compCF: CashflowComponent;

  constructor(compDC: DailyConsolidatedComponent,  compCF: CashflowComponent){
    this._compDC = compDC;
    this._compCF = compCF;
    this.CreateConnection();
    this._compDC.registerOnServerEvents(this._hubConnection);
    this._compCF.registerOnServerEvents(this._hubConnection);
    this.startConnection();
  }
  connectToMessageBroker(){
    this._hubConnection.invoke('ConnectToMessageBroker');
  }

  private CreateConnection(){
    this._hubConnection = new HubConnectionBuilder()
                              .withUrl("http://localhost:5200/hubs/brokerhub")
                              .build();
  }

  private startConnection() : void {
    this._hubConnection
    .start()
    .then(()=> {
      console.log('Hub connection started');
      this.connectToMessageBroker();
      this._compDC.GetInitial();
      this._compCF.GetInitial();
    })
    .catch(()=> {
      setTimeout(() => { this.startConnection();}, 5000);
    });
  }
}
