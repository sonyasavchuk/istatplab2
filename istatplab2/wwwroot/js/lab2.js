const uri = 'api/Playlists'; // наше посилання за яким ми отримаємо список наших об'єктів
let playlists = []; // глобальна змінна для зберігання лекторів

function getPlaylists() {
    fetch(uri) 
        .then(response => {
            if(!response.ok){ // перевіряє чи все ок з запросом
                return response.text().then(text => { throw new Error(text) })} // якщо ні, кидає експешн
            document.getElementById('errorDB').innerHTML = "";
            return response.json();}) // повертає джсон
        .then(data => _displayPlaylists(data))  // викликає функцію для виведення та збереження 
        .catch(error => document.getElementById('errorDB').innerHTML = error.toString());
}

function addPlaylist() {
    // Отримує дані з інпутів за id 
    const addNameTextbox = document.getElementById('add-Name');
    const addTracksTextbox = document.getElementById('add-Tracks');

    
    const playlist = {
        Name: addNameTextbox.value.trim(),
        TracksIds: addTracksTextbox.value.replaceAll(" ", "").split(",")
    };
    // метод POST
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(playlist)
    })
        .then(response => {
            if(!response.ok){ // перевіряє чи все ок з запросом
                return response.text().then(text => { throw new Error(text) })} // якщо ні, кидає експешн
            document.getElementById('errorDB').innerHTML = "";
            return response.json();}) // повертає джсон
        .then(() => {
            getPlaylists(); // отримує нових лекторів
            addNameTextbox.value = ''; //очищає комірки інпутів
            addTracksTextbox.value = '';
        })
        .catch(error => document.getElementById('errorDB').innerHTML = error.toString());
}

function deletePlaylist(id) {
    
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getPlaylists())
        .catch(error => document.getElementById('errorDB').innerHTML = error.toString());
}

function displayEditForm(id) {
    // пошук за id 
    const playlist = playlists.find(playlist => playlist.id === id);
    // вставляє дані в форми
    document.getElementById('edit-Id').value = playlist.id;
    document.getElementById('edit-Name').value = playlist.name;
    document.getElementById('edit-Tracks').value = playlist.tracksIds;
    document.getElementById('editPlaylist').style.display = 'block';
}

function updatePlaylist() {
    // метод PUT
    const playlistId = document.getElementById('edit-Id').value; // бере ID
    const playlist = {
        id: parseInt(playlistId, 10),
        Name: document.getElementById('edit-Name').value.trim(), //string.trim() прибирає пробіли з кінців
        // користувач вводить id організацій через кому, воно розпарсує цей string за допомогою string.split
        TracksIds: document.getElementById('edit-Tracks').value.replaceAll(" ", "")
            .split(",")
            .map(str => {
                return Number(str)
            })
    }
    //передає в контроллер
    fetch(`${uri}/${playlistId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(playlist)
    })
        .then(response => {
            if(!response.ok){ // перевіряє чи все ок з запросом
                return response.text().then(text => { throw new Error(text) })} // якщо ні, кидає експешн
            document.getElementById('errorDB').innerHTML = "";
            })
        .then(()=>getPlaylists())
        // запрос змін
        .catch(error => document.getElementById('errorDB').innerHTML = error.toString());

    closeInput(); // приховує поле змін

    return false;
}

function closeInput() {
    // приховує елемент з редагуванням 
    document.getElementById('editPlaylist').style.display = 'none';
    document.getElementById('errorDB').innerHTML='';
}


function _displayPlaylists(data) {
    const tBody = document.getElementById('playlists');  // отримує тіло таблиці
    tBody.innerHTML = '';


    const button = document.createElement('button'); // створює шаблон для кнопки

    data.forEach(playlist => {  
        
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Редагувати';
        // виклик функції на клік, що заповнює редагуючі поля
        editButton.setAttribute('onclick', `displayEditForm(${playlist.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Видалити';
// виклик функції видалення на клік
        deleteButton.setAttribute('onclick', `deletePlaylist(${playlist.id})`);

        let tr = tBody.insertRow(); // вставляємо стрічку
        // заповнюємо данними
        
        let td0  = tr.insertCell(0); //tr.InsertCell(int) вставляє в указану комірку (починаючи з 0) дані
        let textNodeId = document.createTextNode(playlist.id);
        td0.appendChild(textNodeId);

        let td1 = tr.insertCell(1);
        let textNodeName = document.createTextNode(playlist.name);
        td1.appendChild(textNodeName);
        
        let td30 = tr.insertCell(2);
        let textNodeTracks = document.createTextNode(playlist.tracks.join(",  "));
        td30.appendChild(textNodeTracks);

        let td4 = tr.insertCell(3); //вставляємо кнопку редагування
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4); // вставляємо кнопку видалення
        td5.appendChild(deleteButton);
    });

    playlists = data; // вносимо дані в змінну
}

