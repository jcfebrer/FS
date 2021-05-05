<%
'****************************************************************************************
'**  Copyright Notice
'**
'**  Web Wiz Guide - Web Wiz Forums
'**
'**  Copyright 2001-2004 Bruce Corkhill All Rights Reserved.
'**
'**  This program is free software; you can modify (at your own risk) any part of it
'**  under the terms of the License that accompanies this software and use it both
'**  privately and commercially.
'**
'**  All copyright notices must remain in tacked in the scripts and the
'**  outputted HTML.
'**
'**  You may use parts of this program in your own private work, but you may NOT
'**  redistribute, repackage, or sell the whole or any part of this program even
'**  if it is modified or reverse engineered in whole or in part without express
'**  permission from the Usuarios.
'**
'**  You may not pass the whole or any part of this application off as your own work.
'**
'**  All links to Web Wiz Guide and powered by logo's must remain unchanged and in place
'**  and must remain visible when the pages are viewed unless permission is first granted
'**  by the copyright holder.
'**
'**  This program is distributed in the hope that it will be useful,
'**  but WITHOUT ANY WARRANTY; without even the implied warranty of
'**  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR ANY OTHER
'**  WARRANTIES WHETHER EXPRESSED OR IMPLIED.
'**
'**  You should have received a copy of the License along with this program;
'**  if not, write to:- Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom.
'**
'**
'**  No official support is available for this program but you may post support questions at: -
'**  http://www.webwizguide.info/forum
'**
'**  Support questions are NOT answered by email ever!
'**
'**  For correspondence or non support questions contact: -
'**  info@webwizguide.info
'**
'**  or at: -
'**
'**  Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom
'**
'****************************************************************************************


'Global
'---------------------------------------------------------------------------------
Const portal.variablesForum.strTxtForumHelp = "Ayuda del Foro"
Const portal.variablesForum.strTxtChooseAHelpTopic = "Elija un tema de ayuda"
Const portal.variablesForum.strTxtLoginAndRegistration = "Registr�ndose y Conect�ndose al foro"
Const portal.variablesForum.strTxtUserPreferencesAndForumSettings = "Preferencias de Usuario y Configuraci�n del Foro"
Const portal.variablesForum.strTxtPostingIssues = "Colocando mensajes"
Const portal.variablesForum.strTxtMessageFormatting = "Formateo de Mensajes"
Const portal.variablesForum.strTxtUsergroups = "Grupos de Usuarios"
Const portal.variablesForum.strTxtPrivateMessaging = "Mensajer�a Privada"

Const portal.variablesForum.strTxtWhyCantILogin = "�Porqu� no puedo conectarme?"
Const portal.variablesForum.strTxtDoINeedToRegister = "�Qu� necesito para registrarme?"
Const portal.variablesForum.strTxtLostclaves = "Perdida de Contrase�as"
Const portal.variablesForum.strTxtIRegisteredInThePastButCantLogin = "Estoy registrado pero no puedo conectarme"

Const portal.variablesForum.strTxtHowDoIChangeMyForumSettings = "�C�mo puedo cambiar mi configuraci�n del Foro?"
Const portal.variablesForum.strTxtForumTimesAndDates = "Horario y fecha no est�n igual que mi horario local"
Const portal.variablesForum.strTxtWhatDoesMyRankIndicate = "�Qu� indica mi rango?"
Const portal.variablesForum.strTxtCanIChangeMyRank = "�Puedo cambiar mi rango?"

Const portal.variablesForum.strTxtHowPostMessageInTheForum = "�C�mo puedo colocar un mensaje en el Foro?"
Const portal.variablesForum.strTxtHowDeletePosts = "�C�mo puedo borrar un mensaje?"
Const portal.variablesForum.strTxtHowEditPosts = "�C�mo puedo editar un mensaje?"
Const portal.variablesForum.strTxtHowSignaturToMyPost = "�C�mo puedo a�adir firma a mis mensajes?"
Const portal.variablesForum.strTxtHowCreatePoll = "�C�mo puedo crear una encuesta?"
Const portal.variablesForum.strTxtWhyNotViewForum = "�Porqu� no puedo ver un foro?"
Const portal.variablesForum.strTxtInternetExplorerWYSIWYGPosting = "Usando el Editor WYSIWYG en Internet Explorer"

Const portal.variablesForum.strTxtWhatForumCodes = "�Qu� son los C�digos del Foro?"
Const portal.variablesForum.strTxtCanIUseHTML = "�Puedo usar HTML?"
Const portal.variablesForum.strTxtWhatEmoticons = "�Qu� son las Caritas?"
Const portal.variablesForum.strTxtCanPostImages = "�Puedo colocar im�genes?"
Const portal.variablesForum.strTxtWhatClosedTopics = "�Qu� son los temas cerrados?"

Const portal.variablesForum.strTxtWhatForumAdministrators = "�Qu� es el Administrador del Foro?"
Const portal.variablesForum.strTxtWhatForumModerators = "�Qu� son los Moderadores del Foro?"
Const portal.variablesForum.strTxtWhatUsergroups = "�Qu� son los Grupos de usuarios?"

Const portal.variablesForum.strTxtIPrivateMessages = "No puedo enviar Mensajes Privados"
Const portal.variablesForum.strTxtIPrivateMessagesToSomeUsers = "No puedo enviar Mensajes Privados a algunos usuarios"
Const portal.variablesForum.strTxtHowCanPreventSendingPrivateMessages = "C�mo puedo evitar que alguien me env�e Mensajes Privados"


Const portal.variablesForum.strTxtFAQ1 = "Para conectarse al Foro debe usar el Nombre de Usuario y la Contrase�a que proporcion� cuando se registr�. Si Ud. no est� registrado entonces primero debe hacerlo para poder conectarse. Si ya est� registrado y a�n es incapaz de conectarte verifique que tiene habilitada la opci�n de Cookies en su Navegador Web, puede a�adir este Sitio Web a su lista de Sitios Web confiables. Si Ud. esta bloqueado (banned) en los foros esto puede evitar que se pueda conectar, en tal caso p�ngase en contacto con el Administrador del Foro"
Const portal.variablesForum.strTxtFAQ2 = "Ud. necesita una cantidad m�nima de datos para registrarse, de los cuales muchos son opcionales, pero s�, tiene que estar dispuesto a considerar esa informaci�n como p�blica."
Const portal.variablesForum.strTxtFAQ3 = "Si pierde su contrase�a no se preocupe. Aunque las contrase�as no pueden ser recuperadas si pueden ser reseteadas. Para resetear su contrase�a oprima el bot�n de Iniciar Sesi�n y al final de la P�gina de Inicio de Sesi�n encontrar� un link a la P�gina de Contrase�as perdidas y ah� podr� solicitar que se le env�e una nueva v�a e-mail. Si esta opci�n no esta disponible o no tiene una direcci�n de e-mail valida en su Perfil entonces necesitar� contactar al Administrador del Foro y pedirle que cambie su Contrase�a."
Const portal.variablesForum.strTxtFAQ4 = "Esto puede ocurrir porque no ha colocado mensajes por un largo periodo, o nunca ha colocado un mensaje. Esto es com�n porque el Administrador peri�dicamente borra usuarios inactivos de la base de datos para reducir su tama�o."
Const portal.variablesForum.strTxtFAQ5 = "Puede cambiar su configuraci�n dentro de el Foro editando su Perfil, una vez que se ha conectado a los Foros, Ud. tendr� un bot�n de Editar Perfil el cual puede ser usado para editar su Perfil y cambiar su configuraci�n."
Const portal.variablesForum.strTxtFAQ6 = "Los tiempos usados en los Foros es la hora y fecha del Servidor, Si el servidor esta localizado en otro pa�s entonces la hora y fecha ser�n los de ese pa�s. Para cambiar hora y fecha a su horario local simplemente edite su perfil e indique cuantas horas hay de m�s o de menos respecto a su horario y el del Servidor. Los Foros no esta dise�ados para ajustarse al horario de verano autom�ticamente, as� que necesitar� ajustar su tiempo local durante esos meses."
Const portal.variablesForum.strTxtFAQ7 = "El rango en los Foros indica el grupo de usuarios al cual Ud. pertenece, por ejemplo, Moderadores y Administradores pueden tener un rango especial. Dependiendo de la configuraci�n del Foro Ud. ser� capaz de usar diferentes caracter�sticas del Foro dependiendo del rango al que pertenezca."
Const portal.variablesForum.strTxtFAQ8 = "Si, Ud. Puede cambiar su rango de manera muy simple. Dentro de los Foros hay 8 rangos de los cuales 6 pueden ser accedidos por Ud. Seg�n su n�mero de publicaciones:<br />- Visita (0 estrellas); Si Ud. Pertenece a este rango solo puede acceder a los foros, pero no publicar nada ni votar en las encuestas.<br />- Novato (1 estrella); Si Ud. Pertenece a este rango puede acceder y publicar mensajes en los Foros y tambi�n votar en las encuestas.<br />- Frecuente (2 estrellas); Si Ud. Pertenece a este grupo es porque ya lleva 200 mensajes publicados. Este rango le permite acceder y publicar en los Foros, votar en las encuestas y subir im�genes en sus mensajes.<br />- Avanzado (3 estrellas); Si Ud. Pertenece a este rango es porque ya lleva 400 mensajes publicados. Este rango le permite acceder y publicar en los Foros, votar en las encuestas y subir im�genes y archivos en sus mensajes.<br />- Experto (4 estrellas); Si Ud. Pertenece a este rango es porque ya lleva 1000 mensajes publicados. Este rango le permite acceder y publicar en los Foros, votar en las encuestas, subir im�genes y archivos en sus mensajes y puede publicar encuestas.<br />- Maestro (5 estrellas); Si Ud. Pertenece a este rango es porque ya lleva 2000 mensajes publicados. Este rango le permite acceder y publicar en los Foros, votar en las encuestas, subir im�genes y archivos en sus mensajes y publicar encuestas. Si Ud. Pertenece a este rango puede solicitarle al Administrador del Foro el permiso para ser moderador de uno o algunos de los Foros.<br />- Moderador (5 estrellas); Si Ud. Pertenece a este rango es porque el Administrador lo design� como tal. Este rango le permite acceder y publicar en los Foros, votar en las encuestas, subir im�genes y archivos en sus mensajes, publicar encuestas y modificar, borrar o mover mensajes de los Foros.<br />- Administrador (5 estrellas); Ud. No puede pertenecer a este rango. Pero como informaci�n agregada, el administrador puede hacer cualquiera de las funciones ya mencionadas en los grupos anteriores y adem�s cambiar las configuraciones generales del Foro."
Const portal.variablesForum.strTxtFAQ9 = "Para colocar un mensaje en los Foros oprima el bot�n correspondiente (tema nuevo). Para poder colocar mensajes tiene que estar conectado. Las facilidades disponibles para ti en cada Foro son enumeradas al final del la p�gina del tema que est�s viendo."
Const portal.variablesForum.strTxtFAQ10 = "A menos que sea un Moderador o un Administrador no podr� borrar los mensajes pero si podr� reportarlos a un Moderador o al Administrador."
Const portal.variablesForum.strTxtFAQ11 = " A menos que sea un Moderador o un Administrador no podr� borrar los mensajes pero si podr� reportarlos a un Moderador o al Administrador."
Const portal.variablesForum.strTxtFAQ12 = "Ud. puede a�adir una firma al final de sus mensajes. Para hacer esto necesita primero crear una firma en su Perfil, una vez que ha hecho esto puede a�adir su firma al final de sus mensajes indic�ndolo as� en la casilla de 'Mostrar Firma' al final del formulario para colocar mensajes o marcando la opci�n mostrar siempre la firma del formulario de registro."
Const portal.variablesForum.strTxtFAQ13 = "Si tiene suficientes privilegios para crear una encuesta en un Foro observar� un bot�n llamado 'Encuesta' en la parte superior de la pantalla de foros y temas. Cuando crea una encuesta necesita introducir una pregunta y al menos dos opciones. Tambi�n puede seleccionar si la gente puede votar varias ocasiones o solo una vez."
Const portal.variablesForum.strTxtFAQ14 = "Algunos Foros est�n configurados para permitir a solo ciertos usuarios o grupos acceder a ellos. Para ver, leer, colocar mensajes, etc. en el foro necesita primero permisos los cuales solo un Moderador o Administrador del Foro pueden otorgarle."
Const portal.variablesForum.strTxtFAQ15 = "Si esta usando Internet Explorer 5 o superior en su PC, Ud. podr� tener un editor de mensajes WYSIWYG para teclear sus mensajes. Si encuentra que tiene problemas colocando mensajes usando el editor WYSIWYG entonces deshabilite este editor simplemente editando su Perfil e indique que no desea usar el editor WYSIWYG para colocar mensajes."
Const portal.variablesForum.strTxtFAQ16 = "Los c�digos del Foro le permiten formatear los mensajes que coloca. Los c�digos del Foro son muy similares al HTML excepto que las etiquetas est�n encerradas en par�ntesis cuadrados, [ y ], m�s bien que, &lt; y &gt;. Ud. puede deshabilitar los c�digos del Foro cuando coloca un mensaje. <a href=""JavaScript:openWin('forum_codes.aspx','codes','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=550,height=400')""> Presione aqu� para ver los c�digos del Foro que est�n disponibles</a>"
Const portal.variablesForum.strTxtFAQ17 = "HTML no puede ser usado en los mensajes, esto por razones de seguridad, codigo HTML malicioso puede ser usado para destruir le estructura del Foro o inhabilitar un navegador web cuando un usuario intenta ver o colocar un mensaje."
Const portal.variablesForum.strTxtFAQ18 = "Las caritas son peque�os im�genes que pueden ser usadas para expresar sentimientos o mostrar emociones. Ud. podr� verlos a en el formulario para colocar mensajes. Para a�adir una carita a su mensaje simplemente presione la que te guste."
Const portal.variablesForum.strTxtFAQ19 = "Puede a�adir im�genes a sus mensajes. Si tiene los permisos necesarios para subir im�genes puede enviar una desde su propia computadora a su mensaje. Sin embargo, si la imagen subida no esta disponible entonces necesitar� hacer un link a la imagen que quiere y que esta almacenada en un Servidor Publico accesible, ejemplo. http://www.misitio.com/mi_imagen.jpg"
Const portal.variablesForum.strTxtFAQ20 = "Los temas son cerrados por alguna raz�n por el Administrador del Foro � un Moderador. Una vez que un tema es cerrado no ser� capaz para responder mensajes en ese tema o votar en su encuesta."
Const portal.variablesForum.strTxtFAQ21 = "El Administrador del Foro es una persona que tiene altos niveles de control sobre los Foros, �l tiene la habilidad para permitir o deshabilitar caracter�sticas de los Foros, inhabilitar (bannear) usuarios, borrar usuarios, editar y borrar mensajes, crear grupos de usuarios, etc."
Const portal.variablesForum.strTxtFAQ22 = "Los Moderadores son individuos o grupos de usuarios que se ocupan del funcionamiento cotidiano de los Foros. Ellos tienen el poder para editar, borrar, mover, cerrar, rehabilitar, temas y mensajes, en los Foros que ellos moderan. Los moderadores generalmente son los que previenen que la gente coloque material ofensivo o abusivo."
Const portal.variablesForum.strTxtFAQ23 = "Los grupos de usuarios son una manera de agrupar usuarios. Cada usuario es un miembro de un grupo de usuarios y cada grupo puede ser asignado con derechos individuales en los Foros, para leer, ver, colocar, crear encuestas, etc."
Const portal.variablesForum.strTxtFAQ24 = "Hay varias razones para esto, Ud. no est� conectado, no esta registrado, o el Administrador del Foro tiene deshabilitado el sistema de mensajer�a privada."
Const portal.variablesForum.strTxtFAQ25 = "Esto quiz� porque la persona a quien esta intentando enviar un mensaje privado le ha bloqueado para no recibir mensajes privados. Si este es el caso recibir� un mensaje inform�ndole de esto si intenta enviarle un mensaje privado."
Const portal.variablesForum.strTxtFAQ26 = "Si encuentra que esta recibiendo mensajes privados indeseables de un usuario Ud. puede bloquearlo para que no pueda enviarle esos mensajes. Para hacer esto dir�jase al sistema de Mensajer�a Privada y entre a su lista de amigos. Vea y a�ada el usuario como amigo, pero seleccione la opci�n de la lista desplegable 'No mensajearme', esto previene que este usuario pueda enviarle mensajes privados."
%>