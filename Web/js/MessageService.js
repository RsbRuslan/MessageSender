class MessageService {
    constructor(){
        this._httpService = new HttpService();
        this._messageId = '';
        this._recepientCounter = 1;
    }

    apply() {
        console.log(document.getElementsByClassName('sada'));
        document.getElementById('send_messag_button').addEventListener('click', _ => this._sendMessage(_));
        document.getElementById('check_status_button').addEventListener('click', _ => this._checkStatus(_));
        document.getElementById('add_recepient_button').addEventListener('click', _ => this._appendNewRecepient(_));        
    }

    //#region Handlers
    _appendNewRecepient(event){
        let newRecepient = this._drawRecepientItem(this._recepientCounter++);
        document.getElementById('recepients_container').appendChild(newRecepient);
    }

    async _sendMessage(){
        let message = {};
        message.Recepients = [];
        let recepientsNodes = document.getElementsByClassName('recepient_url_holder');
        for (let item of recepientsNodes) {
            message.Recepients.push(item.value);
        }
        message.Subject = document.getElementById('subject_holder').value;
        message.Body = document.getElementById('message_body_holder').value;

        let address = document.getElementById('service_address_holder').value;
        message.IsDelivered = false;
        let response = await this._httpService.post(address + '/Message', message);
        let json = await response.json();
        alert(`request successed: ${json.isTransactionSuccessed}`);
        if(json.isTransactionSuccessed === true){
            this._messageId = json.data.messageId;
            alert(`message id ${this._messageId}`);
        }
    }

    async _checkStatus(){
        if(this._messageId === '')
        return;

        let address = document.getElementById('service_address_holder').value;
        let response = await this._httpService.get(`${address}/Message/messagestatistics/${this._messageId}`);
        let json = await response.json();
        alert(json.isTransactionSuccessed);
    }
    //#endregion
   
    //#region Drawing
    _drawRecepientItem(recepientPosition){
        let recepientHolder = document.createElement('div');
        recepientHolder.appendChild(this._createButton('Remove', () => recepientHolder.parentNode.removeChild(recepientHolder)));
        recepientHolder.appendChild(this._createInput('recepient_' + recepientPosition, "recepient_url_holder"));
        return recepientHolder;
    }

    _createInput(id, className, defaultValue){
        let input = document.createElement('input');
        input.type = 'text';
        input.id = id;
        input.className = className;
        input.value = defaultValue;
        return input;
    }

    _createButton(buttonText, handler){
        let button = document.createElement('button');
        button.innerHTML = buttonText;
        button.addEventListener('click', handler);
        return button;
    }

    //#endregion
  
    
}

let messageService = new MessageService();
messageService.apply();
