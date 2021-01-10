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
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  computed: {
    alert() {
      return this.$store.state.alert;
    },
  },
  watch: {
    $route(to, from) {
      // clear alert on location change
      this.$store.dispatch("alert/clear");
    },
  },
};
</script>

<style lang="css" scoped></style>
