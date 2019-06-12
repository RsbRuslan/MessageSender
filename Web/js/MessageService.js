
class MessageService {
    constructor(){
        this._httpService = new HttpService();
        this._messageId = '';
    }

    drowPage() {
        let body = document.body;

        body.appendChild(this._createHeader('Message Service Url :'));
        body.appendChild(this._createInput('service_address_holder'));

        body.appendChild(this._createHeader('Subject :'));
        body.appendChild(this._createInput('subject_holder')); 

        body.appendChild(this._createHeader('Body :'));

        let bodyArea = document.createElement('textarea');
        bodyArea.id = 'message_body_holder';
        body.appendChild(bodyArea);

        body.appendChild(this._createHeader('Recepients :'));

        body.appendChild(this._drawRecepientSection());
        
        body.appendChild(document.createElement('br'))

        body.appendChild(this._createButton("Send",() => this._sendMessage(this)));

        body.appendChild(this._createHeader('Check status at notifications :'));

        body.appendChild(this._createButton("CheckStatus",() => this._checkStatus(this)));
    }

    _drawRecepientSection(){
        let recepientCounter = 0;
        let recepientSection = document.createElement('div');
        recepientSection.className = 'recepient_section';

        recepientSection.appendChild(this._createButton("Add recepient", () => {
            let newRecepient = this._drawRecepientItem(recepientCounter++);
            recepientSection.appendChild(newRecepient);
        }));

        recepientSection.appendChild(this._drawRecepientItem(recepientCounter++));
        return recepientSection;
    }

    _drawCheckStatusSection(){
        
    }

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

    _createHeader(text){
        let h = document.createElement('h3');
        h.innerHTML = text;
        return h;
    }

    _createButton(buttonText, handler){
        let button = document.createElement('button');
        button.innerHTML = buttonText;
        button.addEventListener('click', handler);
        return button;
    }

    _removeElement(elem) {
        return recepientHolder.parentNode.removeChild(elem);
    }

    async _sendMessage(self){
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
        let response = await self._httpService.post(address + '/Message', message);
        let json = await response.json();
        alert(`request successed: ${json.isTransactionSuccessed}`);
        if(json.isTransactionSuccessed === true){
            this._messageId = json.data.messageId;
            alert(`message id ${this._messageId}`);
        }
    }


    async _checkStatus(self){
        if(self._messageId === '')
        return;

        let address = document.getElementById('service_address_holder').value;
        let response = await self._httpService.get(`${address}/Message/status/${self._messageId}`);
        let json = await response.json();
        alert(json.data.message.isDelivered);
    }
}

let messageService = new MessageService();
messageService.drowPage();
