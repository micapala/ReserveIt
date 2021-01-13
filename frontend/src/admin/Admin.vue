<template>
  <div>
  <div v-if="alert.message" :class="`alert ${alert.type}`">{{ alert.message }}</div>
  <div class="admin_page">
    <div class="entity">
      <div class="entity_header">
        <div class="entity_title">Create or update Entities</div>
      </div>
      <div class="entity_navbar">
        <div @click="selectedBand()" class="entity_navbar_item">Band</div>
        <div @click="selectedConcert()" class="entity_navbar_item">Concert</div>
      </div>
      <div class="entity_body">
        <form @submit.prevent="handleSubmit">
          <div class="form-group">
            <label for="ID">ID</label>
            <input
              type="number"
              v-model="ID"
              name="ID"
              class="form-control"
            />
          </div>
          <div class="form-group">
            <label for="name">Name</label>
            <input
              type="text"
              v-model="name"
              name="name"
              class="form-control"
              :class="{ 'is-invalid' : submitted && !name }"
            />
            <div v-show="submitted && !name" class="invalid-feedback">
              Name is required!
            </div>
          </div>
          <div class="form-group" v-if="selected=='concert'">
            <label for="bandName">Name of the band playing</label>
            <input
              type="text"
              v-model="bandName"
              name="bandName"
              class="form-control"
              :class="{ 'is-invalid' : submitted && !bandName }"
            />
            <div v-show="submitted && !bandName" class="invalid-feedback">
              Name of the band playing is required!
            </div>
          </div>
          <div class="form-group" v-if="selected=='concert'">
            <label for="price">Ticket price</label>
            <input
              type="number"
              v-model="price"
              name="price"
              class="form-control"
              :class="{ 'is-invalid' : submitted && !price }"
            />
            <div v-show="submitted && !price" class="invalid-feedback">
              Ticket price is required!
            </div>
          </div>
          <div class="form-group" v-if="selected=='concert'">
            <label for="date">Date</label>
            <input
              type="date"
              v-model="date"
              name="date"
              class="form-control"
              :class="{ 'is-invalid' : submitted && !date }"
            />
            <div v-show="submitted && !date" class="invalid-feedback">
              Date is required!
            </div>
          </div>
        </form>
      </div>
      <div class="entity_footer">
        <button @click="handleSubmit">Create/Update</button>
        <button @click="handleDelete">Delete</button>
      </div>
    </div>
    <bandPicker v-model="band"></bandPicker>
    <concertPicker v-model="concert"></concertPicker>
  </div>
  </div>
</template>

<script>
import bandPicker from "../home/BandPicker";
import concertPicker from "../home/ConcertPicker";

export default {
  components: { bandPicker, concertPicker },
  data: () => ({
    band: null,
    concert: null,
    ID: null,
    name: null,
    bandName: null,
    price: null,
    date: null,
    selected: "band",
    submitted: false,
  }),
  watch: {
    band: function() {
      this.selected = "band";
      const { band } = this;
      this.ID = band.id;
      this.name = band.name;
    },
    concert: function() {
      this.selected = "concert";
      const { concert } = this;
      this.ID = concert.id;
      this.name = concert.name;
      this.bandName = concert.bandName;
      this.price = concert.ticketPrice;
      this.date = concert.date.split('T')[0];
    }
  },
  methods: {
    handleSubmit() {
      this.submitted = true;
      const { ID, name, bandName, price, date } = this;
      if(ID) {
      // UPDATE
        if(this.selected == "band" && name)
          this.$store.dispatch("bands/update", {
            id: ID,
            name: name
          });
        if(this.selected == "concert" && name && bandName && price && date)
          this.$store.dispatch("concerts/update", {
            id: ID,
            name: name,
            bandName: bandName,
            price: price,
            date: date,
          });
      } else {
      // CREATE
        if(this.selected == "band" && name)
          this.$store.dispatch("bands/create", {
            name: name
          });
        if(this.selected == "concert" && name && bandName && price && date)
          this.$store.dispatch("concerts/create", {
            name: name,
            bandName: bandName,
            price: price,
            date: date,
          });
      }
    },
    handleDelete() {
      this.submitted = true;
      const { ID } = this;
      if(ID) {
        if(this.selected == "band") {
          this.$store.dispatch("bands/remove", { id: ID });
          this.message = "Deleted";
        }
        if(this.selected == "concert")
          this.$store.dispatch("concerts/remove", { id: ID });
      }
    },
    selectedBand() {
      this.selected = "band";
      this.clearForms();
    },
    selectedConcert() {
      this.selected = "concert";
      this.clearForms();
    },
    clearForms() {
      this.ID = null;
      this.name = null;
      this.bandName = null;
      this.price = null;
      this.date = null;
    }
  },
  computed: {
    alert() {
      return this.$store.state.alert;
    },
  },
};
</script>

<style lang="css" scoped>
.admin_page {
  width: 90%;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-evenly;
  margin-top: 5%;
  color: var(--font-color);
}
.alert {
  max-width: 960px;
  margin: 2rem auto;
  text-align: center;
  box-shadow: 0px 14px 45px rgba(0, 0, 0, 0.25),
    0px 10px 18px rgba(0, 0, 0, 0.22);
}
.entity {
  margin: 2rem;
  width: 390px;
  box-shadow: 0px 14px 45px rgba(0, 0, 0, 0.25),
    0px 10px 18px rgba(0, 0, 0, 0.22);
  background: var(--background-color);
}
.entity_header {
  padding: 15px 10px;
  background: var(--primary-color);
  color: var(--font-color);
  text-transform: uppercase;
}
.entity_title {
  font-size: 1.2rem;
  line-height: 1.5rem;
}
.entity_navbar {
  width: 100%;
  background-color: var(--navbar-color);
  display: flex;
  justify-content: space-evenly;
  padding: 0;
}
.entity_navbar_item {
  display: block;
  color: var(--font-color);
  text-align: canter;
  padding: 0.5rem 2rem;
  cursor: pointer;
}
.entity_navbar_item:hover {
  background-color: var(--primary-color);
  color: black;
}
.entity_body {
  min-height: 420px;
}
.form-group {
  margin-left: 1rem;
  width: 90%;
}
.entity_footer {
  text-align: right;
}
.entity_footer button {
  padding: 8px 10px;
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
.entity_footer button:hover {
  opacity: 1;
}
</style>
