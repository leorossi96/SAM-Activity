from server import db, login_manager
from flask_login import UserMixin


@login_manager.user_loader
def load_user(user_id):
    return User.query.get(int(user_id))


class User(db.Model, UserMixin):
    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String(20), unique=True, nullable=False)
    email = db.Column(db.String(120), unique=True, nullable=False)
    image_file = db.Column(db.String(20), nullable=False, default='default.jpg')
    password = db.Column(db.String(60), nullable=False)
    patients = db.relationship('Patient', backref='therapist', lazy=True)

    def __repr__(self):
        return f"User('{self.username}', '{self.email}', '{self.image_file}')"


class Patient(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    last_name = db.Column(db.String(20), nullable=False)
    first_name = db.Column(db.String(20), nullable=False)
    date_of_birth = db.Column(db.Date, nullable=False)
    type_of_disability = db.Column(db.String(50), nullable=False)
    comment = db.Column(db.String(250))
    user_id = db.Column(db.Integer, db.ForeignKey('user.id'), nullable=False)
    levels_run = db.relationship('LevelRun', lazy=True)
    levels_search = db.relationship('LevelSearch', lazy=True)

    def __repr__(self):
        return f"User('{self.last_name}', '{self.first_name}', '{self.date_of_birth}', '{self.type_of_disability}')"


class LevelRun(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String, unique=True, nullable=False, default='default')
    static_obstacle = db.Column(db.Integer, nullable=False, default=10)
    power_up = db.Column(db.Integer, nullable=False, default=10)
    dynamic_obstacle = db.Column(db.Integer, nullable=False, default=10)
    max_time = db.Column(db.Float, nullable=False, default=100.0)
    lives = db.Column(db.Integer, nullable=False, default=10)
    patient_id = db.Column(db.Integer, db.ForeignKey('patient.id'), nullable=False)

    def __repr__(self):
        return f"User('{self.name}', '{self.static_obstacle}', '{self.power_up}', '{self.dynamic_obstacle}', " \
            f"'{self.max_time}', '{self.lives}')"


class LevelSearch(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String, unique=True, nullable=False, default='default')
    number_search_zone = db.Column(db.Integer, nullable=False, default=1)
    #static_obstacle = db.Column(db.Integer, nullable=False, default=10)
    #power_up = db.Column(db.Integer, nullable=False, default=10)
    #dynamic_obstacle = db.Column(db.Integer, nullable=False, default=10)
    #max_time = db.Column(db.Float, nullable=False, default=100.0)
    #lives = db.Column(db.Integer, nullable=False, default=10)
    patient_id = db.Column(db.Integer, db.ForeignKey('patient.id'), nullable=False)

    def __repr__(self):
        return f"User('{self.name}', '{self.number_search_zone}', '{self.power_up}')"

'''    
class ZoneLevelSearch(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    number_stars_per_zone = db.Columns(db.Integer, nullable=False, default=1)

    

'''
