import type { Person } from './Person';

export class Fundraiser {
  id?:number;
  name: string;
  goalValue: number;
  currentlyRaisedAmount?: number;
  dueDate: Date;
  creationDate?: Date;
  status?: Status;
  owner: Person;
  donors?: Person[];

  constructor(
    name: string,
    goalValue: number,
    dueDate: Date,
    owner: Person,
    id?:number,
    creationDate?: Date,
    status?: Status,
    currentlyRaisedAmount?: number,
    donors?: Person[],
  ) {
    this.name = name;
    this.goalValue = goalValue;
    this.currentlyRaisedAmount = currentlyRaisedAmount;
    this.creationDate = creationDate;
    this.dueDate = dueDate;
    this.status = status;
    this.owner = owner;
    this.donors = donors;
    this.id=id;
  }
}

export const enum Status {
  'Closed' = 'Closed',
  'Active' = 'Active',
}