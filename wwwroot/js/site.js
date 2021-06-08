const uri = 'Employees';
let todos = [];
const depUri = 'Departments';

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addSurNameTextbox = document.getElementById('add-sname');
    const addAgeTextbox = document.getElementById('add-age');
    const addGenderTextbox = document.getElementById('add-gender');
    const addDepTextbox = document.getElementById('add-dep');
    const addLangTextbox = document.getElementById('add-lang');


    const item = {
        name: addNameTextbox.value.trim(),
        surname: addSurNameTextbox.value.trim(),
        age: addAgeTextbox.value,
        gender: addGenderTextbox.value,
        departmentId: addDepTextbox.value,
        programmingLanguageId: addLangTextbox.value
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.id === id);

    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-sname').value = item.surname;
    document.getElementById('edit-age').value = item.age;
    document.getElementById('edit-gender').value = item.gender;
    document.getElementById('edit-dep').value = item.departmentId;
    document.getElementById('edit-lang').value = item.programmingLanguageId;


    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        name: document.getElementById('edit-name').value.trim(),
        surname: document.getElementById('edit-sname').value.trim(),
        age: document.getElementById('edit-age').value,
        gender: document.getElementById('edit-gender').value,
        departmentId: document.getElementById('edit-dep').value,
        programmingLanguageId: document.getElementById('edit-lang').value
    };

    fetch(uri, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Сотрудник' : 'Сотрудников';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('emplList');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(item.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        td2.appendChild(document.createTextNode(item.surname));

        let td3 = tr.insertCell(2);
        td3.appendChild(document.createTextNode(item.age));

        let td4 = tr.insertCell(3);
        td4.appendChild(document.createTextNode(item.gender));

        let td5 = tr.insertCell(4);
        td5.appendChild(document.createTextNode(item.departmentName));

        let td6 = tr.insertCell(5);
        td6.appendChild(document.createTextNode(item.programmingLanguage));

        let td8 = tr.insertCell(6);
        td8.appendChild(editButton);

        let td9 = tr.insertCell(7);
        td9.appendChild(deleteButton);
    });

    todos = data;
}
