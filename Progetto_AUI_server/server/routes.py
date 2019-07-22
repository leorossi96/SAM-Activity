import os
import json
import secrets
import requests
from PIL import Image
from flask import render_template, url_for, flash, redirect, request, jsonify
from server import app, db, bcrypt
from server.form import RegistrationForm, LoginForm, UpdateAccountForm, PatientForm
from server.models import User, Patient, LevelRun
from flask_login import login_user, current_user, logout_user, login_required


@app.route("/")
@app.route("/home")  # route decoder to navigate our web application. In this case the slash / is simply the root
def home():
    if current_user.is_authenticated:
        if len(current_user.patients) > 0:
            patients = current_user.patients
            for p in patients:
                print('patient_id {}'.format(p.id))
                print(type(p.id))
            return render_template('home.html', patients=patients)
    return render_template('home.html')


@app.route("/prova", methods=['GET', 'POST'])
def prova():
    if current_user.is_authenticated:
        if len(current_user.patients) > 0:
            patients = current_user.patients
            ser_list = []
            for pat in patients:
                ser = pat.__dict__
                del ser['_sa_instance_state']
                ser_list.append(ser)
            return json.dumps(ser_list, default=str)
    return 'no patients'


@app.route("/prova/levels/run", methods=['GET', 'POST'])
def prova_levels_run():
    pat_data = request.get_json()
    patient = Patient.query.get(pat_data['id'])
    levels_run = patient.levels_run
    lr_list = []
    for lr in levels_run:
        lrl = lr.__dict__
        del lrl['_sa_instance_state']
        lr_list.append(lrl)
    return json.dumps(lr_list, default=str)


@app.route("/prova/levels/search", methods=['GET', 'POST'])
def prova_levels_search():
    pat_data = request.get_json()
    patient = Patient.query.get(pat_data['id'])
    levels_search = patient.levels_search
    ls_list = []
    for ls in levels_search:
        lsl = ls.__dict__
        del lsl['_sa_instance_state']
        ls_list.append(lsl)
    return json.dumps(ls_list, default=str)


@app.route("/prova/save/run", methods=['GET', 'POST'])
def prova_save_data():
    save_data = request.get_json()
    lr = (LevelRun.query.filter_by(id=save_data['id']).first())
    print(lr)
    lr.lives = save_data['lives']
    print(lr)
    db.session.commit()
    print(Patient.query.get(save_data['patient_id']).levels_run)
    #lr = Patient.query.get(save_data['patient_id']).levels_run
    #for l in lr:
    #    if l.id == save_data['id']:
    #        l.lives = save_data['lives']
    #lr.get(save_data['id']).lives = save_data['lives']
    #db.session.commit()
    #print(Patient.query.get(save_data['patient_id']).levels_run)
    #patient = Patient.query.get(save_data['patient_id'])
    #lev_runs = patient.levels_run
    #for lr in lev_runs:
    #    if lr.id == save_data['id']:
    #        lr.lives = save_data['lives']
    #        db.session.commit()
    #print(patient.levels_run)
    return 'speriamo bene'





@app.route("/about")  # route decoder to navigate our web application. In this case the slash / is simply the root
def about():
    return render_template('about.html')


@app.route("/register", methods=['GET', 'POST'])  # route decoder to navigate our web application. In this case the slash / is simply the root
def register():
    if current_user.is_authenticated:
        return redirect(url_for('home'))
    form = RegistrationForm()
    if form.validate_on_submit():
        hashed_password = bcrypt.generate_password_hash(form.password.data).decode('utf-8')
        user = User(username=form.username.data, email=form.email.data, password=hashed_password)
        db.session.add(user)
        db.session.commit()
        flash('Your account has been created! You are now able to login!', 'success')
        return redirect(url_for('login'))

    return render_template('register.html', title='Register', form=form)


@app.route("/login", methods=['GET', 'POST'])  # route decoder to navigate our web application. In this case the slash / is simply the root
def login():
    if current_user.is_authenticated:
        return redirect(url_for('home'))
    form = LoginForm()
    if form.validate_on_submit():
        user = User.query.filter_by(email=form.email.data).first()
        if user and bcrypt.check_password_hash(user.password, form.password.data):
            login_user(user, remember=form.remember.data)
            next_page = request.args.get('next')
            return redirect(next_page) if next_page else redirect(url_for('home'))
        else:
            flash('Login Unsuccessful, Please check username and password', 'danger')
    return render_template('login.html', title='Login', form=form)


@app.route("/login/unity", methods=['GET', 'POST', 'PUT'])
def login_unity():
    login_data = request.get_json()
    print('Ho ricevuto la richiesta da unity')
    email = login_data['email']
    password = login_data['password']
    print(email)
    print(password)
    user = User.query.filter_by(email=email).first()
    if user and bcrypt.check_password_hash(user.password, password):
        patients = user.patients
        ser_list = []
        for pat in patients:
            ser = pat.__dict__
            del ser['_sa_instance_state']
            ser_list.append(ser)
        return json.dumps(ser_list, default=str)
    else:
        return "login_unsuccessful!"




@app.route("/logout")  # route decoder to navigate our web application. In this case the slash / is simply the root
def logout():
    logout_user()
    return redirect(url_for('home'))


def save_picture(form_picture):         #create a random name for the image in order to avoid to collide with image already present
    random_hex = secrets.token_hex(8)
    _, f_ext = os.path.splitext(form_picture.filename)
    picture_fn = random_hex + f_ext
    picture_path = app.root_path + os.sep + 'static' + os.sep + 'profile_pics'+ os.sep + picture_fn
    output_size = (125, 125)
    i = Image.open(form_picture)
    i.thumbnail(output_size)
    i.save(picture_path)
    return picture_fn


# route decoder to navigate our web application. In this case the slash / is simply the root
@app.route("/account", methods=['GET', 'POST'])
@login_required
def account():
    form = UpdateAccountForm()
    if form.validate_on_submit():
        if form.picture.data:
            picture_file = save_picture(form.picture.data)
            current_user.image_file = picture_file
        current_user.username = form.username.data
        current_user.email = form.email.data
        db.session.commit()
        flash('Your Account has been Updated!', 'success')
        return redirect(url_for('account'))
    elif request.method == 'GET':
        form.username.data = current_user.username
        form.email.data = current_user.email
    image_file = url_for('static', filename='profile_pics/' + current_user.image_file)
    return render_template('account.html', title='Account', image_file=image_file, form=form)


@app.route("/patient/new", methods=['GET', 'POST'])  # route decoder to navigate our web application. In this case the slash / is simply the root
@login_required
def new_patient():
    form = PatientForm()
    if form.validate_on_submit():
        patient = Patient(last_name=form.last_name.data, first_name=form.first_name.data,
                          date_of_birth=form.date_of_birth.data, type_of_disability=form.type_of_disability.data,
                          comment=form.comment.data, therapist=current_user)
        db.session.add(patient)
        db.session.commit()
        flash('successful', 'success')
        return redirect(url_for('home'))
    return render_template('new_patient.html', title='Add New Patient', form=form)


@app.route("/patient/<id_p>", methods=['GET', 'POST'])
@login_required
def patient(id_p):
    if current_user.is_authenticated:
        id_reg = id_p.replace('}', '')
        id_int = int(id_reg)
        print(id_int)
        print(type(id_int))
        patients = current_user.patients
        for p in patients:
            if p.id == id_int:
                pat = p
        levels_run = pat.levels_run
        levels_search = pat.levels_search
        return render_template('patient.html', patient=pat, lev_run=levels_run, lev_search=levels_search)







