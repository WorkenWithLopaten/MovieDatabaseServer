"use strict";var userControllers={get:function(n,t){return{login:function(){n.signIn().then(function(n){localStorage.setItem("currentUser",JSON.stringify(n.user))}).catch(function(n){localStorage.removeItem("currentUser");var t=n.message;console.log("Could not login: ",t)})},signOut:function(){n.signOut().then(function(){app.currentUser=null}).catch(function(n){console.log("Error "+n)})}}}};