var baseurl = "https://localhost:44390/api";

function getContact(guid){
$.get(baseurl + "/Contact/GetContact/" + guid, function(data, status){
	$("#guid").val(data.guid); 
	$("#firstName").val(data.firstName); 
	$("#LastName").val(data.lastName); 
	$("#email").val(data.email); 
	$("#PhoneNumber").val(data.phoneNumber); 
	
	$('#saveContact').hide();
	$('#updatecontact').show();
	}); 
}

function getAllContacts(){
	$.get(baseurl + "/Contact/GetAllContacts" , function(data, status){
	for (var i = 0; i < data.length; i++) {
		var row$ = $('<tr>');
		row$.append('<th>' + data[i]["firstName"] + " " + data[i]["lastName"] + '</th>');
		row$.append('<th>' + data[i]["phoneNumber"] + '</th>');
		row$.append('<th>' + data[i]["email"] + '</th>');
		row$.append('<th>' + "<button class='Actionbtn' onclick='editContact(\"" + data[i]["guid"] + "\")'>View / Edit</button>    <button class='Actionbtn' onclick='deleteContact(\"" + data[i]["guid"] + "\")'>Delete</button></th>");
		row$.append('</tr>');
		$("#idUserList > table").append(row$);
	  }
  });
}
		
function editContact(id){  
  window.location.href = "./AddContact.html?id=" + id;
}

function deleteContact(id){
	$.ajax({
		url: baseurl + "/Contact/DeleteContact/" + id,
		type: 'DELETE',
		success: function(result) {
			window.location.reload();
		}
	});
}

function saveContact(){
	var settings = {
	  "url": baseurl + "/contact/AddContact",
	  "method": "POST",
	  "headers": {
		"Content-Type": "application/json"
	  },
	  "data": JSON.stringify({
		"firstName": $("#firstName").val(),
		"lastName": $("#LastName").val(),
		"email": $("#email").val(),
		"phoneNumber": $("#PhoneNumber").val()
	  }),
	};

	$.ajax(settings).always(function (response) {
	  alert(response);
	});
}
	
function updateContact(){
	var settings = {
	  "url": baseurl + "/contact/EditContact",
	  "method": "POST",
	  "headers": {
		"Content-Type": "application/json"
	  },
	  "data": JSON.stringify({
		"guid": $("#guid").val(),
		"firstName": $("#firstName").val(),
		"lastName": $("#LastName").val(),
		"email": $("#email").val(),
		"phoneNumber": $("#PhoneNumber").val()
	  }),
	};

	$.ajax(settings).done(function (response) {
	  alert(response);
	});
}