<template>
  <div class="band_picker">
    <div class="picker_header">
      <div class="picker_title">Bands</div>
    </div>
    <div class="picker_body">
      <img
        id="loading"
        v-show="!bands.items"
        src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
      />
      <div class="band_items" v-if="bands.items">
        <div
          :class="['band_item', { selected: active(band) }]"
          v-for="band in sortedBands"
          :key="band.id"
          @click="select(band)"
        >
          {{ band.name }}
        </div>
      </div>
    </div>
    <div class="picker_footer"></div>
  </div>
</template>

<script>
export default {
  name: "band_picker",
  model: {
    prop: "value",
    event: "change"
  },
  data() {
    return {
      selected: null
    };
  },
  computed: {
    bands() {
      return this.$store.state.bands.all;
    },
    sortedBands() {
      return this.bands.items.sort((a, b) => {
        let fa = a.name.toLowerCase(),
          fb = b.name.toLowerCase();
        if (fa < fb) {
          return -1;
        } else {
          return 1;
        }
        return 0;
      });
    }
  },
  created() {
    this.$store.dispatch("bands/getAll");
  },
  methods: {
    select(band) {
      if(this.selected != band) {
        this.selected = band;
        this.$emit("change", band);
      }
    },
    active(band) {
      return this.selected === band;
    }
  }
};
</script>

<style lang="css" scoped>
.band_picker {
  margin-top: 2rem;
  width: 260px;
  top: 100%;
  box-shadow: 0px 14px 45px rgba(0, 0, 0, 0.25),
    0px 10px 18px rgba(0, 0, 0, 0.22);
  background: var(--background-color);
}
.picker_header {
  padding: 15px 10px;
  background: var(--primary-color);
  color: var(--font-color);
  text-transform: uppercase;
}
.picker_title {
  font-size: 1.2rem;
  line-height: 1.5rem;
}
.picker_body {
  max-height: 520px;
  overflow-y: auto;
  color: var(--font-color);
}
.band_item {
  width: 90%;
  text-align: center;
  margin: 5px 0px 0px 10px;
  padding: 5px 0px 5px;
  background-color: var(--navbar-color);
  display: block;
  cursor: pointer;
}
.band_item.selected {
  background-color: var(--primary-color);
}
.band_item:hover {
  background-color: var(--purple-color);
}
.picker_footer {
  padding: 10px;
}
img#loading {
  padding: 20px 10px 10px 120px;
}
</style>
