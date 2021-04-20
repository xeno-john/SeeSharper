﻿using Memento;
using Shared;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace View
{
	using Strategy;
  public partial class View : Form, IView
	{
		#region Private Members
		private IPresenter _presenter;
		private Image _drawing;
		private PaintEventHandler _currentAddedPaintHandler;
		private Button _selectedColorButton;
		#endregion
		#region Public Member Functions
		public View()
    {
      InitializeComponent();
    }

		public void SetPresenter(IPresenter presenter)
		{
			_presenter = presenter;
		}
		public void CaptureDrawingState()
		{
			_drawing = pictureBox.Image;
		}
		public void SetDrawingMemento(DrawingMemento drawingMemento)
		{
			_drawing = drawingMemento.Drawing;
		}
		public DrawingMemento GetDrawingMemento()
		{
			return new DrawingMemento(_drawing);
		}
		public void ChangeCurrentHandler(Strategy strategy)
		{
			RemoveCurrentHandler();
			AddHandler(strategy);
		}
		public void AddHandler(Strategy strategy)
		{
			_currentAddedPaintHandler = strategy.GetDraw();
			Paint += _currentAddedPaintHandler;
		}
		public void RemoveCurrentHandler()
		{
			Paint -= _currentAddedPaintHandler;
		}
		#endregion
		#region Private Member Functions
		private void colorButton1_Click(object sender, EventArgs e)
		{
			colorButton1.FlatStyle = FlatStyle.Popup;
			colorButton2.FlatStyle = FlatStyle.Standard;
			colorButton3.FlatStyle = FlatStyle.Standard;
			_selectedColorButton = colorButton1;
			_presenter.ColorChanged(_selectedColorButton.BackColor);
		}

		private void colorButton2_Click(object sender, EventArgs e)
		{
			colorButton1.FlatStyle = FlatStyle.Standard;
			colorButton2.FlatStyle = FlatStyle.Popup;
			colorButton3.FlatStyle = FlatStyle.Standard;
			_selectedColorButton = colorButton2;
			_presenter.ColorChanged(_selectedColorButton.BackColor);
		}

		private void colorButton3_Click(object sender, EventArgs e)
		{
			colorButton1.FlatStyle = FlatStyle.Standard;
			colorButton2.FlatStyle = FlatStyle.Standard;
			colorButton3.FlatStyle = FlatStyle.Popup;
			_selectedColorButton = colorButton3;
			_presenter.ColorChanged(_selectedColorButton.BackColor);
		}

		private void editColorButton_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() != DialogResult.OK)
				return;

			Color color = colorDialog.Color;
			_selectedColorButton.BackColor = color;
			_presenter.ColorChanged(color);
		}

		private void pictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			_presenter.MouseClicked(e.X, e.Y);
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			_presenter.MouseMoved(e.X, e.Y);
		}

		private void redoButton_Click(object sender, EventArgs e)
		{
			_presenter.Redo();
		}

		private void undoButton_Click(object sender, EventArgs e)
		{
			_presenter.Undo();
		}
    #endregion
  }
}
