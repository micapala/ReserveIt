<template>
  <div class="main_column">
    <div class="forms_container">
      <div v-if="alert.message" :class="`alert ${alert.type}`">
        {{ alert.message }}
      </div>
      <div>
        <h1 id="registration">Registration</h1>
        <form @submit.prevent="handleSubmit">
          <div class="form-group">
            <label for="username">Username</label>
            <input
              type="text"
              v-model="username"
              name="username"
              class="form-control"
              :class="{ 'is-invalid': submitted && !username }"
            />
            <div v-show="submitted && !username" class="invalid-feedback">
              Username is required
            </div>
          </div>
          <div class="form-group">
            <label for="password">Password</label>
            <input
              type="password"
              v-model="password"
              name="password"
              class="form-control"
              :class="{ 'is-invalid': submitted && !password }"
            />
            <div v-show="submitted && !password" class="invalid-feedback">
              Password is required
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
              E-mail adress is needed
            </div>
          </div>
          <div class="form-group">
            <label for="name">Name</label>
            <input
              type="text"
              v-model="name"
              name="name"
              class="form-control"
              :class="{ 'is-invalid': submitted && !name }"
            />
            <div v-show="submitted && !name" class="invalid-feedback">
              Name is needed
            </div>
          </div>
          <div class="form-group">
            <label for="surname">Surname</label>
            <input
              type="text"
              v-model="surname"
              name="surname"
              class="form-control"
              :class="{ 'is-invalid': submitted && !surname }"
            />
            <div v-show="submitted && !surname" class="invalid-feedback">
              Name is needed
            </div>
          </div>
          <div class="form-group">
            <button class="btn btn-primary" :disabled="signingIn">
              Confirm
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
      submitted: false
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
      return this.$store.state.registration.status.loading;
    }
  },
  watch: {
    $route() {
      this.$store.dispatch("alert/clear");
    }
  }
};
</script>

<style lang="css" scoped>
h1#registration {
  color: var(--font-color);
}
</style>
