from server import db, login_manager
from flask_login import UserMixin
from datetime import time, datetime


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
    image_file = db.Column(db.String(20), nullable=False, default='default.jpg')
    user_id = db.Column(db.Integer, db.ForeignKey('user.id'), nullable=False)
    levels_run = db.relationship('LevelRun', lazy=True)
    levels_search = db.relationship('LevelSearch', lazy=True)
    sessions = db.relationship('Session', lazy=True)

    def __repr__(self):
        return f"Patient('{self.last_name}', '{self.first_name}', '{self.date_of_birth}', '{self.type_of_disability}')"


class LevelRun(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String, nullable=False, default='default')
    static_obstacle = db.Column(db.Integer, nullable=False, default=5)
    power_up = db.Column(db.Integer, nullable=False, default=1)
    dynamic_obstacle = db.Column(db.Integer, nullable=False, default=2)
    max_time = db.Column(db.Float, nullable=False, default=60.0)
    lives = db.Column(db.Integer, nullable=False, default=2)
    patient_id = db.Column(db.Integer, db.ForeignKey('patient.id'), nullable=False)

    def __repr__(self):
        return f"Level_Run('{self.name}', '{self.static_obstacle}', '{self.power_up}', '{self.dynamic_obstacle}', " \
            f"'{self.max_time}', '{self.lives}')"


class LevelSearch(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String, nullable=False, default='default')
    level_time = db.Column(db.Time, nullable=False, default=time(0,0,0,0))
    patient_id = db.Column(db.Integer, db.ForeignKey('patient.id'), nullable=False)
    zone_levels = db.relationship('ZoneLevelSearch', lazy=True)

    def __repr__(self):
        return f"Level_Search('{self.name}')"


class ZoneLevelSearch(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    number = db.Column(db.Integer, nullable=False, default=1)
    number_stars_per_zone = db.Column(db.Integer, nullable=False, default=1)
    level_search_id = db.Column(db.Integer, db.ForeignKey('level_search.id'), nullable=False)

    def __repr__(self):
        return f"Zone_Level_Search('{self.number_stars_per_zone}', '{self.number_stars_per_zone}')"


class Session(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    patient_id = db.Column(db.Integer, db.ForeignKey('patient.id'), nullable=False)
    datetime = db.Column(db.DateTime, nullable=False, default=datetime(2019, 9, 28, 23, 55, 59, 342380))
    session_runs = db.relationship('SessionRun', lazy=True)
    session_searches = db.relationship('SessionSearch', lazy=True)

    def __repr__(self):
        return f"Session('{self.id}', '{self.patient_id}')"


class SessionSearch(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    level_time = db.Column(db.Time, nullable=False, default=time(0,0,0,0))
    image_file = db.Column(db.String(150), nullable=False, default='default.jpg')
    session_id = db.Column(db.Integer, db.ForeignKey('session.id'), nullable=False)

    def __repr__(self):
        return f"SessionSearch('{self.id}', '{self.level_time}', '{self.session_id}',  '{self.image_file}')"


class SessionRun(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    level_time = db.Column(db.Time, nullable=False, default=time(0,0,0,0))
    life_remaining = db.Column(db.Integer, nullable=False, default=0)
    activated_power_up = db.Column(db.Integer, nullable=False, default=0)
    session_id = db.Column(db.Integer, db.ForeignKey('session.id'), nullable=False)

    def __repr__(self):
        return f"SessionSearch('{self.id}', '{self.level_time}', '{self.life_remaining}', " \
            f"'{self.activated_power_up}', '{self.session_id}')"