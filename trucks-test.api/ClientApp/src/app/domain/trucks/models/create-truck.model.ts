import { Validators } from '@angular/forms';

export class CreateTruck {
  public order: string;

  public truckType: string;

  public fabricationYear: number;

  public modelYear: number;

  public name: string;

  public plate: string;
}

export const FormOptions = {
  order: ["", [Validators.minLength(10)]]
};

export const FormValidations = {
};

