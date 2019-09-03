from flask_wtf import FlaskForm
from flask_wtf.file import FileField, FileAllowed
from flask_login import current_user
from wtforms import StringField, PasswordField, SubmitField, BooleanField, TextAreaField, DateField, IntegerField, FloatField, FieldList
from wtforms.validators import DataRequired, Length, Email, EqualTo, ValidationError
from server.models import User


class RegistrationForm(FlaskForm):
    username = StringField('Username', validators=[DataRequired(), Length(min=4, max=20)])
    # 'Username' is gonna be used as reference in the html
    email = StringField('Email', validators=[DataRequired(), Email()])
    password = PasswordField('Password', validators=[DataRequired(), Length(min=6, max=50)])
    confirm_password = PasswordField('Confirm Password', validators=[DataRequired(), EqualTo('password')])
    submit = SubmitField('Sign Up')

    def validate_username(self, username):
        user = User.query.filter_by(username=username.data).first()
        if user:
            raise ValidationError('The username is taken. Please choose a different one.')

    def validate_email(self, email):
        user = User.query.filter_by(email=email.data).first()
        if user:
            raise ValidationError('The email is taken. Please choose a different one.')


class LoginForm(FlaskForm):
    email = StringField('Email', validators=[DataRequired(), Email()])
    password = PasswordField('Password', validators=[DataRequired(), Length(min=8, max=50)])
    remember = BooleanField('Remember Me')
    submit = SubmitField('Login')


class UpdateAccountForm(FlaskForm):
    username = StringField('Username', validators=[DataRequired(), Length(min=4, max=20)])
    # 'Username' is gonna be used as reference in the html
    email = StringField('Email', validators=[DataRequired(), Email()])
    picture = FileField('Update Profile Picture', validators=[FileAllowed(['jpg', 'png', 'jpeg'])]) # we can add also other extension
    submit = SubmitField('Update')

    def validate_username(self, username):
        if username.data != current_user.username:
            user = User.query.filter_by(username=username.data).first()
            if user:
                raise ValidationError('The username is taken. Please choose a different one.')

    def validate_email(self, email):
        if email.data != current_user.email:
            user = User.query.filter_by(email=email.data).first()
            if user:
                raise ValidationError('The email is taken. Please choose a different one.')


class PatientForm(FlaskForm):
    last_name = StringField('Last Name', validators=[DataRequired()])
    first_name = StringField('First Name', validators=[DataRequired()])
    date_of_birth = DateField('Date of Birth (YYYY-MM-DD)', validators=[DataRequired()])
    type_of_disability = StringField('Type of Disability', validators=[DataRequired()])
    comment = TextAreaField('Comment', validators=[Length(min=0, max=250)])
    submit = SubmitField('Add Patient')


class UpdatePatientForm(FlaskForm):
    last_name = StringField('Last Name', validators=[DataRequired()])
    first_name = StringField('First Name', validators=[DataRequired()])
    date_of_birth = DateField('Date of Birth', validators=[DataRequired()])
    type_of_disability = StringField('Type of Disability', validators=[DataRequired()])
    comment = TextAreaField('Comment', validators=[DataRequired(), Length(min=0, max=250)])
    picture = FileField('Update Profile Picture', validators=[FileAllowed(['jpg', 'png', 'jpeg'])]) # we can add also other extension
    submit = SubmitField('Update Patient')


class UpdateLevelRunForm(FlaskForm):
    static_obstacle = IntegerField('Static Obstacle', validators=[DataRequired()])
    power_up = IntegerField('Power Up', validators=[DataRequired()])
    dynamic_obstacle = IntegerField('Dynamic Obstacle', validators=[DataRequired()])
    max_time = FloatField('Max Time', validators=[DataRequired()])
    lives = IntegerField('Lives', validators=[DataRequired()])
    submit = SubmitField('Update Level Run')


class UpdateLevelSearchForm(FlaskForm):
    number_stars_per_zone = FieldList(IntegerField('Number of stars for Collectible Area', validators=[DataRequired()]),
                                      min_entries=2, max_entries=10)
    submit = SubmitField('Update Level Search')


