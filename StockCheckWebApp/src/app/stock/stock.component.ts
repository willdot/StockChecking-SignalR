import { Component, OnInit } from '@angular/core';
import { StockService } from '../services/stock.service';
import { StockItems } from '../models/StockItems';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {

  constructor(private _stockService: StockService) { }

  itemAmount = 0;
  requestedAmount = 0;
  selectedItem = '';
  errorMessage = '';

  private items: StockItems[] = [];

  ngOnInit() {
    this.getStock();
  }

  getStock(): void {
    this._stockService.GetStock()
      .subscribe(data => {
        this.items = data;

        if (this.selectedItem !== '') {
          this.itemAmount = this.items.find(p => p.name === this.selectedItem).amount;
        }
      }
      );

    console.log(`Stock: ${this.items}`);
  }

  onRemoveStockClicked(): void {
    const newItem: StockItems = {
      name: this.selectedItem,
      amount: this.requestedAmount
    };

    this._stockService.RemoveStock(newItem)
      .subscribe(
        data => {
          this.errorMessage = '';
          this.getStock();
        },
        error => {
          const message: HttpErrorResponse = <any>error;
          this.errorMessage = message.error;
        });
  }

  onAddStockClicked(): void {
    const newItem: StockItems = {
      name: this.selectedItem,
      amount: this.requestedAmount
    };

    this._stockService.AddStock(newItem)
      .subscribe(
        data => {
          this.errorMessage = '';
          this.getStock();
        },
        error => {
          const message: HttpErrorResponse = <any>error;
          this.errorMessage = message.error;
        });
  }

  onSelectedItem(item): void {
    console.log(`You selected: ${item}`);
    this.itemAmount = this.items.find(p => p.name === item).amount;
    this.selectedItem = item;
  }

  onClicked(): void {
  }
}
