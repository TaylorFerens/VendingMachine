export class Coin {
  name: string;
  value: number;
  coinType: number;
  quantity: number;

  constructor(name:string, value:number, coinType:number) {
    this.name = name;
    this.value = value;
    this.coinType = coinType;
    this.quantity = 0;
  }
}
