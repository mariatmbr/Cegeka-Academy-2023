import { Fundraiser, Status } from '../Models/Fundraiser';
import { Person } from '../Models/Person';
import type Donation from '../Models/Donation';
import axios from 'axios';
export class FundraiserService {
  private apiUrl: string = 'https://localhost:7075';

  public async getAll(): Promise<Fundraiser[]> {
    try {
      const response = await axios.get(this.apiUrl + '/Fundraiser');
      return response?.data;
    } catch (e) {
      console.log(e);
    }

    return [];
  }

  public async getOne(id: number): Promise<Fundraiser | null> {
    try {
      const response = await axios.get(this.apiUrl + `/Fundraiser/${id}`);
      const objectReceived: IdentifiableFundraiserDTO = response?.data;
      const owner = new Person(
        objectReceived.owner.name,
        objectReceived.owner.idNumber,
        objectReceived.owner.dateOfBirth,
      );
      const donors: Person[] = [];
      objectReceived.donors.forEach((donor) => {
        donors.push(new Person(donor.name, donor.idNumber,objectReceived.owner.dateOfBirth,));
      });

      return new Fundraiser(
        objectReceived.name,
        objectReceived.goalValue,
        objectReceived.dueDate,
        owner,
        objectReceived.id,
        objectReceived.creationDate,
        objectReceived.status,
        objectReceived.currentlyRaisedAmount,
        donors,
      );
    } catch (e) {
      console.log(e);
    }
    return null;
  }

  public async createFundraiser(fundraiser: Fundraiser): Promise<number|null> {
    try {
       const response=await axios.post(this.apiUrl + '/Fundraiser',fundraiser);
        return response?.data;
    } catch (e) {
      console.log(e);
    }
    return null;
  }

  public async donateToFundraiser(fundraiserId:number,donation:Donation):Promise<boolean>{
    try {
        await axios.post(this.apiUrl + `/Fundraiser/${fundraiserId}/donate`,donation);
        return true;
     } catch (e) {
       console.log(e);
     }
     return false;
  }
}

interface FundraiserDTO {
  name: string;
  goalValue: number;
  currentlyRaisedAmount: number;
  creationDate: Date;
  dueDate: Date;
  status: Status;
}

interface IdentifiableFundraiserDTO extends FundraiserDTO {
  id: number;
  owner: Person;
  donors: Person[];
}