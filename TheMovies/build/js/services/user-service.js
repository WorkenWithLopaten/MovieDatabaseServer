"use strict";var userService={isSignedIn:function(){return null!==JSON.parse(localStorage.getItem("currentUser"))},signIn:function(){return firebase.auth().signInWithPopup(app.provider)},signOut:function(){return firebase.auth().signOut()},sendMessage:function(e){return app.database.ref().child("/messages").push(e)}};