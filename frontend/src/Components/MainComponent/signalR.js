import { HubConnectionBuilder } from '@microsoft/signalr';

let connection;

export const initializeSignalR = (onReceiveChatUpdate) => {


    if (connection) {
        return connection;
    }

    connection = new HubConnectionBuilder()
    .withUrl("http://localhost:5138/chathub", {
        accessTokenFactory: () => {
            return localStorage.getItem('token');
        }
    })
    .withAutomaticReconnect()
    .build();

    connection.start()
        .then(() => {
            console.log("Connected to SignalR hub");

            connection.on("ReceiveChatUpdate", () => {
                console.log("Received chat update");
                onReceiveChatUpdate();
            });
        })
        .catch(error => {
            console.error("Error connecting to SignalR hub:", error);
            alert("Unable to connect to SignalR hub. Check console for details.");
        });

    return connection;
};