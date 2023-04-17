import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({ providedIn: 'root' })
export class BeUrlService {
    public readonly urlAddress = environment.beUrl;
    public readonly checkMessengerUrlAddress = environment.checkMessengerUrl;
}