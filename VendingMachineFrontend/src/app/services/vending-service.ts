import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { environment } from "../../environments/environment";
import retry from 'p-retry';
import { ApiConstants } from "../classes/api-constants";
import { Drink } from "../models/drink";
import { VendingFilter } from "../models/vending-filter";
import { Injectable } from "@angular/core";
import { Order } from "../models/order";


@Injectable({
  providedIn: 'root'
})
export class VendingService {
  constructor(private httpClient: HttpClient) {

  }

  public async GetDrinks(): Promise<Drink[]> {
    let opResult: Drink[] = null;
    let url: string = null
    let params: HttpParams = null;

    // Set the filter to get the drinks from the controller api
    let vendingFilter: VendingFilter = new VendingFilter();

    vendingFilter.getDrinks = true;

    url = `${environment.apiUrlBase}${ApiConstants.API_VENDING_MACHINE}`
    params = this.getQueryParameters(vendingFilter);

    await retry(
      async () => {
        opResult = await this.httpClient
          .get<Drink[]>(url, { params: params })
          .toPromise();
      }
    );
    return opResult;
  }

  public async PlaceOrder(order: Order): Promise<Order> {
    let opResult: Order = null;
    let url: string = null
    let data: string = null;

    // Set the filter to get the drinks from the controller api
    let vendingFilter: VendingFilter = new VendingFilter();
    let header = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    vendingFilter.getDrinks = true;

    url = `${environment.apiUrlBase}${ApiConstants.API_VENDING_MACHINE}`
    data = JSON.stringify(order);

    await retry(
      async () => {
        opResult = await this.httpClient
          .post<Order>(url, data, {headers: header})
          .toPromise();
      }
    );

    return opResult;
  }

  private getQueryParameters(filter: VendingFilter): HttpParams {

    let retval: HttpParams = new HttpParams();

    for (const key in filter) {
      if (filter[key] != null) {
        retval = retval.set(key.toString(), filter[key]);
      }
    }

    return retval;

  }

}
