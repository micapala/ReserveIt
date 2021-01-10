<template>
  <div class="jumbotron">
    <div class="container">
      <div class="row">
        <div class="col-sm-6 offset-sm-3">
          <div v-if="alert.message" :class="`alert ${alert.type}`">
            {{ alert.message }}
          </div>
          <div>
            <h1 id="registration">Rejestracja</h1>
            <form @submit.prevent="handleSubmit">
              <div class="form-group">
                <label for="username">Nazwa użytkownika</label>
                <input
                  type="text"
                  v-model="username"
                  name="username"
                  class="form-control"
                  :class="{ 'is-invalid': submitted && !username }"
                />
                <div v-show="submitted && !username" class="invalid-feedback">
                  Nazwa użytkownika jest wymagana
                </div>
              </div>
              <div class="form-group">
                <label for="password">Hasło</label>
                <input
                  type="password"
                  v-model="password"
                  name="password"
                  class="form-control"
                  :class="{ 'is-invalid': submitted && !password }"
                />
                <div v-show="submitted && !password" class="invalid-feedback">
                  Nie podano hasła
                </div>
              </div>
              <div class="form-group">
                <label for="name">E-mail</label>
                <input
                  type="text"
                  v-model="email"
                  name="email"
                  class="form-control"
                  :class="{ 'is-invalid': submitted && !email }"
                />
                <div v-show="submitted && !email" class="invalid-feedback">
                  Nie podano adresu e-mail
                </div>
              </div>
              <div class="form-group">
                <label for="name">Imie</label>
                <input
                  type="text"
                  v-model="name"
                  name="name"
                  class="form-control"
                  :class="{ 'is-invalid': submitted && !name }"
                />
                <div v-show="submitted && !name" class="invalid-feedback">
                  Nie podano imienia
                </div>
              </div>
              <div class="form-group">
                <label for="surname">Nazwisko</label>
                <input
                  type="text"
                  v-model="surname"
                  name="surname"
                  class="form-control"
                  :class="{ 'is-invalid': submitted && !surname }"
                />
                <div v-show="submitted && !surname" class="invalid-feedback">
                  Nie podano imienia
                </div>
              </div>
              <div class="form-group">
                <button class="btn btn-primary" :disabled="signingIn">
                  Zatwierdź
                </button>
                <img
                  v-show="signingIn"
                  src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
                />
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  data() {
    return {
      username: "",
      password: "",
      email: "",
      name: "",
      surname: "",
      submitted: false,
    };
  },
  methods: {
    handleSubmit() {
      this.submitted = true;
      const { username, password, email, name, surname } = this;
      const { dispatch } = this.$store;
      if (username && password && email && name && surname) {
        dispatch("registration/register", {
          username,
          password,
          email,
          name,
          surname
        });
      }
    }
  },
  computed: {
    alert() {
      return this.$store.state.alert;
    },
    signingIn() {
      return this.$store.state.registration.status.signingIn;
    }
  },
  watch: {
    $route(to, from) {
      // clear alert on location change
      this.$store.dispatch("alert/clear");
    }
  }
};
</script>

<style lang="css" scoped></style>
