import { Validators } from '@angular/forms';

const currentYear = new Date().getFullYear();

export class UpdateTruck {
  public truckType: string;

  public fabricationYear: number;

  public modelYear: number;

  public name: string;

  public plate: string;
};

export const FormOptions = {
  truckType: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(3)]],

  fabricationYear: [currentYear, [Validators.required, Validators.min(currentYear), Validators.max(currentYear)]],
  modelYear: [currentYear, [Validators.required, Validators.min(currentYear), Validators.max(currentYear + 1)]],

  name: [null, [Validators.maxLength(255)]],
  plate: [null, [Validators.maxLength(10)]],
};

export const FormValidations = {
};

export const ValidTruckTypes = [
  'FH', 'FM'
];


