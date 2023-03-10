import type { Person } from './Person';

export class Fundraiser {
  name: string;
  goalValue: number;
  currentlyRaisedAmount?: number;
  dueDate: Date;
  creationDate?: Date;
  status?: Status;
  owner: Person;

  constructor(
    name: string,
    goalValue: number,
    dueDate: Date,
    owner: Person,
    creationDate?: Date,
    status?: Status,
    currentlyRaisedAmount?: number,
  ) 
  
  {
    this.name = name;
    this.goalValue = goalValue;
    this.currentlyRaisedAmount = currentlyRaisedAmount;
    this.creationDate = creationDate;
    this.dueDate = dueDate;
    this.status = status;
    this.owner = owner;
  }
}

export const enum Status {
  'Closed' = 0,
  'Active' = 1,
}
