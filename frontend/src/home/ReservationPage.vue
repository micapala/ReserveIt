<template>
  <div class="main_column">
    <div v-if="alert.message"
         :class="`alert ${alert.type}`"
    >{{ alert.message }}</div>
    <h1>Reservation for {{ reservation.concert_name }}</h1>
    <h2>Band name: {{ reservation.band_name }}</h2>
    <h2>Ticket price: {{ reservation.ticket_price }} PLN</h2>
    <button v-if="!reservation.paymentId" class="btn btn-primary" @click="createReservation()">
      Confirm reservation
    </button>
    <button v-if="reservation.paymentId" class="btn btn-primary" @click="goToPayment()">
      Go to payment
    </button>
  </div>
</template>

<script>
export default {
  computed: {
    reservation() {
      return this.$store.state.reservation;
    },
    alert() {
      return this.$store.state.alert;
    },
  },
  methods: {
    createReservation() {
      var userName = this.$store.state.authentication.user.username;
      var concertId = this.$store.state.reservation.concert_id;

      this.$store.dispatch("reservation/reserve", { userName, concertId });
    },
    goToPayment() {
      var controlString = this.$store.state.reservation.paymentId;
      this.$store.dispatch("payment/goToPayment", { controlString });
    },
  },
  watch: {
    $route(to, from) {
      this.$store.dispatch("reservation/clear");
    }
  }
};
</script>

<style lang="css" scoped>
.alert {
  max-width: 560px;
  margin: 1rem auto;
  text-align: center;
  box-shadow: 0px 14px 45px rgba(0, 0, 0, 0.25),
    0px 10px 18px rgba(0, 0, 0, 0.22);
}
</style>
