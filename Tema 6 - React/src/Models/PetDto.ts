import type { AnimalType } from './Pet'
import type {Person} from './Person'
export interface PetDto {
    birthDate: Date,
    description: string,
    imageUrl: string,
    isHealthy: boolean,
    name: string,
    weightInKg: number,
    type: AnimalType,
    rescuer: Person

}