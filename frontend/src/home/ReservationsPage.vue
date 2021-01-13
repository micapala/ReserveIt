<template>
  <div>
    <md-table md-card>
      <md-table-toolbar>
        <h1 class="md-title">Reservations</h1>
      </md-table-toolbar>

      <md-table-row>
        <md-table-head md-numeric>Reservation Date</md-table-head>
        <md-table-head>Concert Date</md-table-head>
        <md-table-head>Concert Name</md-table-head>
        <md-table-head>Ticket price</md-table-head>
        <md-table-head>Amount paid</md-table-head>
      </md-table-row>

      <md-table-row
        v-for="(item, i) in reservations"
        v-bind:key="i"
        slot="md-table-row"
      >
        <md-table-cell md-label="ID" md-sort-by="reservationDate" md-numeric>{{
          item.reservationDate
        }}</md-table-cell>
        <md-table-cell md-label="Name" md-sort-by="concertName">{{
          item.concertDate
        }}</md-table-cell>
        <md-table-cell md-label="Email" md-sort-by="concertDate">{{
          item.concertName
        }}</md-table-cell>
        <md-table-cell md-label="Gender" md-sort-by="ticketPrice">{{
          item.ticketPrice
        }}</md-table-cell>
        <md-table-cell md-label="Job Title" md-sort-by="amountPaid">{{
          item.amountPaid
        }}</md-table-cell>
      </md-table-row>
    </md-table>
  </div>
</template>

<script>
export default {
  name: "reservations_list",
  created() {
    this.$store.dispatch("reservations/getAll");
  },
  computed: {
    reservations() {
      return this.$store.state.reservations.all.items;
    },
  },
};
</script>
