<template>
  <div class="concert_selection">
    <div class="sidebar">
      <calendar v-model="date"></calendar>
      <bandPicker v-model="band"></bandPicker>
      <concertPicker v-model="concert"></concertPicker>
    </div>
    <div v-if="selected" class="concerts">
      <h1 v-if="selected == 'date'">Concerts on {{ date }}</h1>
      <h1 v-if="selected == 'band'">Concerts with {{ band.name }}</h1>
      <h1 v-if="selected == 'concert'">Selected Concert</h1>
      <h4 v-if="filteredConcerts.length == 0">No concerts found</h4>
      <span v-if="concerts.error" class="text-danger">
        ERROR: {{ concerts.error }}
      </span>
      <div class="concerts_container" v-if="selected">
        <concertitem
          v-for="item in filteredConcerts"
          v-bind:name="item.name"
          v-bind:price="item.ticketPrice"
          v-bind:band_name="item.bandName"
          v-bind:id="item.id"
          v-bind:key="item.id"
        ></concertitem>
      </div>
    </div>
  </div>
</template>

<script>
import calendar from "./Calendar";
import bandPicker from "./BandPicker";
import concertPicker from "./ConcertPicker";
import concertitem from "./concert-item";

export default {
  components: { calendar, bandPicker, concertPicker, concertitem },
  data: () => ({
    date: "",
    band: null,
    concert: null,
    selected: ""
  }),
  computed: {
    concerts() {
      return this.$store.state.concerts.all;
    },
    filteredConcerts() {
      switch(this.selected) {
        case "date": return this.concertsByDate(this.date);
        break;
        case "band": return this.concertsByBand(this.band.name);
        break;
        case "concert": return [this.concert];
        break;
        default: return [];
      }
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
    concert: function() {
      this.selected = "concert";
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
  justify-content: space-between;
}
.concerts {
  text-align: center;
  max-width: calc(100% - 260px);
}
.concerts_container {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
