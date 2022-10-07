import { Component, OnInit } from '@angular/core';
import { ChartType } from 'angular-google-charts';
import { CovidService } from './services/covid.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CovidChartAngular';
  columnNames=['Tarih', 'İstanbul', 'Ankara', 'İzmir' , 'Konya','Antalya'];
  options:any={legend:{position:'Bottom'}};
  type1 = ChartType.LineChart; 
  constructor(public covidService:CovidService){

  }
  ngOnInit(): void {
    this.covidService.startConnection();
    this.covidService.startListener();
  }
 
}
