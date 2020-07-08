export class Contact {
    public constructor(
        public messageID?: number,
        public userID?: string,
        public userFirstName?: string,
        public userLastName?: string,
        public userEmail?: string,
        public userMessage?: string
    ) { }
}