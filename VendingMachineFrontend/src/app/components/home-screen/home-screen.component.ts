import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Coin } from '../../models/coin';
import { Drink } from '../../models/drink';
import { Order } from '../../models/order';
import { VendingService } from '../../services/vending-service';
import { GenericModalComponent } from '../generic-modal/generic-modal.component';

@Component({
  selector: 'app-home-screen',
  templateUrl: './home-screen.component.html',
  styleUrls: ['./home-screen.component.scss']
})
export class HomeScreenComponent implements OnInit {

  public coins: Coin[] = [];
  public drinks: Drink[] = [];
  public order: Order;
  public total: number = 0;
  public totalPaid: number = 0;
  public orderDisabled = false;

  constructor(private vendingService: VendingService,
    private dialog: MatDialog) { }

  async ngOnInit() {
    // add all the coins to the array
    this.initCoins();

    this.drinks = await this.vendingService.GetDrinks();

    this.validateDrinks();
  }

  public updateTotal() {
    this.total = 0;

    this.drinks.forEach(drink => {
      this.total += drink.order * drink.value;
    });

    this.validateDrinks();
  }

  public updateTotalPaid() {
    this.totalPaid = 0;

    this.coins.forEach(coin => {
      this.totalPaid += coin.quantity * coin.value;
    });

    this.validateDrinks();

  }

  public async getDrinks() {

    let notEnoughDrinks: boolean = false;

    this.drinks.forEach(d => {
      if (d.order > d.quantity) {
        notEnoughDrinks = true;
      }
    });

    if (this.totalPaid > this.total && !notEnoughDrinks) {
      // place order
      await this.placeOrder();

      if (this.order != null) {
        this.dialog.open(GenericModalComponent, {
          width: '400px',
          height: '200px',
          disableClose: true,
          data: {
            header: "Order Summary",
            content: this.formatSummary(),
            cancel: "Okay"
          }
        });

        this.drinks = await this.vendingService.GetDrinks();
        this.total = 0;
        this.totalPaid = 0;
        this.initCoins();
        this.drinks.forEach(d => {
          d.order = 0;
        });

        this.validateDrinks();
      }
      else {
        this.dialog.open(GenericModalComponent, {
          width: '400px',
          height: '200px',
          disableClose: true,
          data: {
            header: "Error",
            content: "Insufficient change in machine to fufill your order",
            cancel: "Close"
          }
        });
      }

    }
    else if (this.total == 0) {
      // show error message from insufficient funds
      this.dialog.open(GenericModalComponent, {
        width: '400px',
        height: '200px',
        disableClose: true,
        data: {
          header: "Error",
          content: "You must select at least one beverage.",
          cancel: "Close"
        }
      });
    }
    else if (notEnoughDrinks) {
      this.dialog.open(GenericModalComponent, {
        width: '400px',
        height: '200px',
        disableClose: true,
        data: {
          header: "Error",
          content: "Not enough drinks in inventory.",
          cancel: "Close"
        }
      });
    }
    else {
      this.dialog.open(GenericModalComponent, {
        width: '400px',
        height: '200px',
        disableClose: true,
        data: {
          header: "Error",
          content: "You have insufficient funds to place your order",
          cancel: "Close"
        }
      });
    }
  }

  private validateDrinks() {
    let drinkTotal = 0;
    let selectedDrink = 0;

    this.drinks.forEach(drink => {
      drinkTotal += drink.quantity;
      selectedDrink += drink.order;
    })

    this.orderDisabled = (drinkTotal == 0 || (this.totalPaid < this.total) || this.totalPaid == 0 || selectedDrink == 0);

  }

  private formatSummary(): string {
    let retval: string = "";
    let change: number = 0;

    this.order.drinks.forEach(d => {
      if (d.order > 0) {
        retval += d.order + " " + d.name + "(s)\n";
      }
    });

    this.order.coins.forEach(c => {

      change += (c.quantity * c.value);

    });

    retval += "Your Change total is: " + change;

    return retval;
  }

  private initCoins() {
    this.coins = [];

    this.coins.push(new Coin("Penny", 1, 0), new Coin("Nickel", 5, 1), new Coin("Dime", 10, 2), new Coin("Quarter", 25, 3));
  }
  private async placeOrder() {

    this.order = new Order();
    this.order.drinks = this.drinks;
    this.order.coins = this.coins;

    this.order = await this.vendingService.PlaceOrder(this.order);
  }
}
