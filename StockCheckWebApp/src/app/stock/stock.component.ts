import { Component, OnInit } from '@angular/core';
import { StockService } from '../services/stock.service';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {

  constructor(private _stockService: StockService) { }

  ngOnInit() {
  }

  onClicked(): void {
    this._stockService.AddStock('something')
    .subscribe(
      data => {
        console.log(data);
      }
    );
  }
}
