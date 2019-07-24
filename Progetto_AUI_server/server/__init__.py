from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from flask_bcrypt import Bcrypt
from flask_login import LoginManager

# REMEMBER:
# In order to run the web server through command line, we have to setup the environment variable:
#   MAC: export FLASK_APP=run.py   WIN: set FLASK_APP=run.py
# In order to avoid to shut-down and restart the server every time to display any small changes:
#   we could se the DEBUG MODE -> MAC: export FLASK_DEBUG=1     WIN: set FLASK_DEBUG=1
# RUN WEB SERVER THROUGH MAIN
app = Flask(__name__)
app.config['SECRET_KEY'] = '803e204d9e244ded3ccdc6b34ec49e1d'
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///site.db'
# create a db instances. We can represent out database structure as classes
db = SQLAlchemy(app)
bcrypt = Bcrypt(app)
login_manager = LoginManager(app)
login_manager.login_view = 'login'
login_manager.login_message_category = 'info'


from server import routes
