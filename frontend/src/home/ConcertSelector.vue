<template>
  <div class="concert_selection">
    <div class="sidebar">
      <calendar v-model="date"></calendar>
      <bandPicker v-model="band"></bandPicker>
    </div>
    <div v-if="selected" class="concerts">
      <h1 v-if="selected == 'date'">Concerts on {{ date }}</h1>
      <h1 v-if="selected == 'band'">Concerts with {{ band.name }}</h1>
      <h4 v-if="filteredConcerts.length == 0">No concerts found</h4>
      <span v-if="concerts.error" class="text-danger">
        ERROR: {{ concerts.error }}
      </span>
      <div class="concerts_container" v-if="selected">
        <concertitem
          v-for="concert in filteredConcerts"
          v-bind:name="concert.name"
          v-bind:price="concert.ticketPrice"
          v-bind:band_name="concert.bandName"
          v-bind:id="concert.id"
          v-bind:key="concert.id"
        ></concertitem>
      </div>
    </div>
  </div>
</template>

<script>
import calendar from "./Calendar";
import bandPicker from "./BandPicker";
import concertitem from "./concert-item";

export default {
  components: { calendar, bandPicker, concertitem },
  data: () => ({
    date: "",
    band: null,
    selected: ""
  }),
  computed: {
    concerts() {
      return this.$store.state.concerts.all;
    },
    filteredConcerts() {
      if (this.selected == 'date') return this.concertsByDate(this.date);
      else if (this.selected == 'band') return this.concertsByBand(this.band.name);
      else return [];
    }
  },
  created() {
    this.$store.dispatch("concerts/getAll");
  },
  watch: {
    date: function() {
      this.selected = "date";
      const { date } = this;
    },
    band: function() {
      this.selected = "band";
      const { band } = this;
    },
  },
  methods: {
    concertsByDate(date) {
      return this.concerts.items.filter((item) => {
        return item.date.split('T')[0] == date;
      });
    },
    concertsByBand(band) {
      return this.concerts.items.filter((item) => {
        return item.bandName == band;
      });
    },
  }
};
</script>

<style lang="css" scoped>
.sidebar {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
}
.concert_selection {
  display: flex;
  flex-wrap: wrap;
  margin-top: 5%;
  margin-left: 5%;
}
.concerts {
  color: var(--font-color);
  text-align: center;
  margin-left: 2rem;
}
.concerts_container {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  justify-content: space-between;
  margin-left: 2rem;
  margin-top: 1rem;
}
</style>
