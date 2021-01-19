<template>
  <div class="main_column">
    <h1>Reservations</h1>
    
    <table>
      <tr>
        <th>Reservation Date</th>
        <th>Concert Date</th>
        <th>Concert Name</th>
        <th>Ticket price</th>
        <th>Status</th>
      </tr>

      <tr
        v-for="(item, i) in reservations"
        v-bind:key="i"
      >
        <td>{{ item.reservationDate.split('.')[0].replace('T', ' ') }}</td>
        <td>{{ item.concertDate.split('T')[0] }}</td>
        <td>{{ item.concertName }}</td>
        <td>{{ item.ticketPrice }} PLN</td>
        <!--td>{{ item.amountPaid }}</td-->
        <td>
          <div id="Paid" v-if="item.amountPaid > 0" class="button">Paid</div>
          <a v-if="item.amountPaid == 0" :href="item.paymentLink" target="_blank">
            <div class="button">Retry payment</div>
          </a>
        </td>
      </tr>
    </table>
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

<style lang="css" scoped>
table {
  width: 100%;
  box-shadow: 0px 14px 45px rgba(0, 0, 0, 0.25),
    0px 10px 18px rgba(0, 0, 0, 0.22);
}
th {
  background-color: var(--primary-color);
  padding: 15px 10px;
  text-transform: uppercase;
  font-size: 1.2rem;
  font-weight: normal;
  line-height: 1.5rem;
}
td {
  padding: 6px 32px 6px 10px;
}
tr:hover {
  background-color: var(--purple-color);
}
.button {
  text-transform: uppercase;
  font-weight: bold;
  color: var(--primary-color);
  opacity: 0.9;
  background: transparent;
  outline: none;
  border: 0;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
}
.button:hover {
  opacity: 1;
}
a:hover {
  text-decoration: none;
}
div#Paid {
  cursor: auto;
}
</style>
