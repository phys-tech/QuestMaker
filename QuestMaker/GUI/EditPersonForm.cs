﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using QuestMaker.Classes;

namespace QuestMaker.GUI
{
    public partial class EditPersonForm : Telerik.WinControls.UI.RadForm
    {
        public CPerson editedPerson;
        private CAimManager aimManager;
        private CItemManager itemManager;
        public EditPersonForm(CPerson _person, CAimManager _aimManager, CItemManager _itemManager)
        {
            InitializeComponent();
            editedPerson = _person;
            aimManager = _aimManager;
            itemManager = _itemManager;
            fillUIComponents();
        }

        private void fillUIComponents()
        {
            ddlSex.ValueMember = "EnumSex";
            ddlSex.DisplayMember = "DisplayString";
            ddlSex.DataSource = CPersonManager.list;

            lvAims.Items.Clear();
            lvAims.DataSource = aimManager.getAimsList();
            lvAims.DisplayMember = "DisplayString";
            lvAims.ValueMember = "Id";

            lvItems.Items.Clear();
            lvItems.DataSource = itemManager.getItemsList();
            lvItems.DisplayMember = "DisplayString";
            lvItems.ValueMember = "Id";

            tbName.Text = editedPerson.getName();
            ddlSex.SelectedValue = editedPerson.sex;
            tbcDescription.Text = editedPerson.description;
            cbUnremovable.Checked = editedPerson.unremovable;
            tbcComment.Text = editedPerson.comment;
            tbAltName.Text = editedPerson.altName;
            foreach (ListViewDataItem lvitem in lvItems.Items)
                if (editedPerson.itemsId.Contains((int)lvitem.Value))
                    lvitem.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
            foreach (ListViewDataItem lvaim in lvAims.Items)
                if (editedPerson.aimsId.Contains((int)lvaim.Value))
                    lvaim.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;

        }
        private void getPersonFromUI()
        {
            editedPerson.setName(tbName.Text);
            editedPerson.sex = (Sex) ddlSex.SelectedValue;
            editedPerson.description = tbcDescription.Text;
            editedPerson.unremovable = cbUnremovable.Checked;
            editedPerson.comment = tbcComment.Text;
            editedPerson.altName = tbAltName.Text;

            List<int> items = new List<int>();
            List<int> aims = new List<int>();
            
            foreach (ListViewDataItem lvItem in lvItems.CheckedItems)
                items.Add((int)lvItem.Value);
            foreach (ListViewDataItem lvAim in lvAims.CheckedItems)
                aims.Add((int)lvAim.Value);

            editedPerson.setOwnItems(items);
            editedPerson.setOwnAims(aims);
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            getPersonFromUI();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
